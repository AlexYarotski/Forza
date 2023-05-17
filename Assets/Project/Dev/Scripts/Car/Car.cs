using System;
using Project.Dev.Scripts.Interface;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Car : MonoBehaviour, IDamageable
    {
        public static event Action<Vector3> Died = delegate { };
        public static event Action<Vector3> Drove = delegate { };
        
        private int _health = 0;
        private float _timeOfImmortality = 0;
        private float _speed = 0;
        private float _maxSpeed = 0;
        private float _speedTurn = 0;
        private float _brake = 0;
        private float _boost = 0;
        private float _speedRotation = 0;
        private float _speedReturnRotation = 0;
        private float _rotationAngel = 0;
        private RoadBounds _roadBounds = null;

        private Vector3 _nextPosition = Vector3.zero;
        private Vector3 _dragPosition = Vector3.zero;
        private float _startSpeed = 0;

        private void Awake()
        {
            var carType = (CarModelType)PlayerPrefs.GetInt("Car");
            var carData = SceneContexts.Instance.CarDataSettings.GetCarData(carType);

            _health = carData.Health;
            _timeOfImmortality = carData.TimeOfImmortality;
            _speed = carData.Speed;
            _maxSpeed = carData.MaxSpeed;
            _speedTurn = carData.SpeedTurn;
            _brake = carData.Brake;
            _boost = carData.Boost;
            _speedRotation = carData.SpeedRotation;
            _speedReturnRotation = carData.SpeedReturnRotation;
            _rotationAngel =carData.RotationAngel;
            _roadBounds = carData.RoadBounds;
                
            _startSpeed = _speed;
            _dragPosition = transform.position;
        }

        private void OnEnable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
            Score.Boost += Score_Boost;
        }

        private void OnDisable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
            Score.Boost -= Score_Boost;
        }

        private void Update()
        {
            MoveForward();
            SetTurn();
            
            Drove(transform.position);
        }

        public void GetDamage()
        {
            _health--;

            Vibration.Play();
            
            if (_health <= 0)
            {
                OnDie();
            }
            else if (_health == 1)
            {
                PlaySmoke();    
            }
            
            if (_health >= 1)
            {
                var immortal = new Immortal(this).MakeImmortal(_timeOfImmortality);
                StartCoroutine(immortal);
            }
            
            Brake();
        }

        public void OnDie()
        {
            gameObject.SetActive(false);
            
            StopAllCoroutines();
            
            Died(transform.position);
        }
        
        private void MoveForward()
        {
            var position = transform.position;
            var posAxisZ = position.z + _speed * Time.deltaTime;

            position = new Vector3(position.x, 0, posAxisZ);
            transform.position = position;
        }

        private void SetTurn()
        {
            if (_roadBounds.IsInBounds(_nextPosition.x))
            {
                SetRotation();

                _nextPosition = new Vector3(_nextPosition.x, transform.position.y, transform.position.z);

                SetStartSpeed();
            }
            else
            {
                _nextPosition = _roadBounds.ClampPosition(_nextPosition);
                _nextPosition = new Vector3(_nextPosition.x, transform.position.y, transform.position.z);

                 Brake();
            }
            
            transform.position = _nextPosition;
            _dragPosition = transform.position;
            
            SetStartRotation();
        }

        private void Brake()
        {
            _speed -= _brake * Time.deltaTime;
        }

        private void SwipeController_Dragged(Vector3 dragPositionVector3)
        {
            _nextPosition.x = _dragPosition.x + dragPositionVector3.x * (_speedTurn * Time.deltaTime);
        }

        private void SetStartSpeed()
        {
            if (_speed < _startSpeed && _speed <= _maxSpeed)
            {
                _speed += _brake * Time.deltaTime;
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

            nextRotation.eulerAngles = new Vector3(0, _rotationAngel * delta, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, _speedRotation * Time.deltaTime);
        }

        private void SetStartRotation()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 
                _speedReturnRotation * Time.deltaTime);
        }
        
        private void Score_Boost(float boost)
        {
            if (_speed <= _maxSpeed)
            {
                _startSpeed += _boost;
            }
        }
        
        private void PlaySmoke()
        {
            ParticleManager.Instance.Play(ParticleType.CarSmoke);
        }
    }
}