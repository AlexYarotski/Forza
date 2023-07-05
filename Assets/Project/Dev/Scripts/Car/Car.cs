using System;
using UnityEngine;

public class Car : MonoBehaviour, IDamageable
{
    private const string KeyCar = "Car";
    
    [SerializeField]
    private CarView _carView = null;

    public static event Action<Vector3> Died = delegate { };
    public static event Action<Vector3> Drove = delegate { };

    private int _health = 0;
    private float _speed = 0;
    private float _startSpeed = 0;

    private Vector3 _nextPosition = Vector3.zero;
    private Vector3 _dragPosition = Vector3.zero;
    
    private CarDataSettings.CarData _carData = null;
    
    private void OnEnable()
    {
        SwipeController.Dragged += SwipeController_Dragged;
        Score.Boost += Score_Boost;
    }

    private void OnDisable()
    {
        SwipeController.Dragged -= SwipeController_Dragged;
        Score.Boost -= Score_Boost;
    }

    private void Start()
    {
        var carType = (CarModelType)PlayerPrefs.GetInt(KeyCar);
        _carData = SceneContexts.Instance.CarDataSettings.GetCarData(carType);

        _carView.EnableCarView(carType);
        SetColor(carType);

        _health = _carData.Health;
        _speed = _carData.Speed;

        _startSpeed = _speed;
        _dragPosition = transform.position;
    }
    
    private void Update()
    {
        MoveForward();
        SetTurn();

        Drove(transform.position);
    }

    #region Health

    public void GetDamage()
    {
        _health--;

        Vibration.Play();

        if (_health >= 1)
        {
            StartCoroutine(new Immortal(this).MakeImmortal(_carData.TimeOfImmortality));

            Brake();
        }

        if (_health <= 0)
        {
            OnDie();
        }

        if (_health == 1)
        {
            PlaySmoke();
        }
    }

    public void OnDie()
    {
        gameObject.SetActive(false);

        StopAllCoroutines();

        Died(transform.position);
    }

    #endregion

    #region Movement

    private void MoveForward()
    {
        var position = transform.position;
        var posAxisZ = position.z + _speed * Time.deltaTime;

        position = new Vector3(position.x, 0, posAxisZ);
        transform.position = position;
    }

    private void SetTurn()
    {
        if (_carData.RoadBounds.IsInBounds(_nextPosition.x))
        {
            SetRotation();

            _nextPosition = new Vector3(_nextPosition.x, transform.position.y, transform.position.z);

            SetStartSpeed();
        }
        else
        {
            _nextPosition = _carData.RoadBounds.ClampPosition(_nextPosition);
            _nextPosition = new Vector3(_nextPosition.x, transform.position.y, transform.position.z);

            Brake();
        }

        transform.position = _nextPosition;
        _dragPosition = transform.position;

        SetStartRotation();
    }

    private void Brake()
    {
        _speed -= _carData.Brake * Time.deltaTime;
    }

    private void SwipeController_Dragged(Vector3 dragPositionVector3)
    {
        _nextPosition.x = _dragPosition.x + dragPositionVector3.x * (_carData.SpeedTurn * Time.deltaTime);
    }

    private void SetStartSpeed()
    {
        if (_speed < _startSpeed && _speed <= _carData.MaxSpeed)
        {
            _speed += _carData.Brake * Time.deltaTime;
        }
    }

    private void SetRotation()
    {
        var nextRotation = Quaternion.identity;
        var delta = _nextPosition.x - transform.position.x;

        if (delta.AlmostEquals(delta, 0))
        {
            return;
        }

        nextRotation.eulerAngles = new Vector3(0, _carData.RotationAngel * delta, 0);
        transform.rotation =
            Quaternion.Lerp(transform.rotation, nextRotation, _carData.SpeedRotation * Time.deltaTime);
    }

    private void SetStartRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity,
            _carData.SpeedReturnRotation * Time.deltaTime);
    }

    private void Score_Boost(float boost)
    {
        if (_speed <= _carData.MaxSpeed)
        {
            _startSpeed += _carData.Boost;
        }
    }

    #endregion


    private void SetColor(CarModelType model)
    {
        var colorIndex = PlayerPrefs.GetInt(model.ToString());

        if (_carData.ColorSetting.CheckColor((ColorName)colorIndex))
        {
            _carView.PaintElement(model, (ColorName)colorIndex);
            
            return;
        }
        
        _carView.PaintElement(model, _carData.ColorSetting.ColorConfigs[0].ColorName);
    }

    private void PlaySmoke()
    {
        ParticleManager.Instance.Play(ParticleType.CarSmoke);
    }
}