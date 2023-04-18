using System;
using Project.Dev.Scripts.Interface;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour, IDamageable
    {
        public static event Action<Vector3> Died = delegate { };
        public static event Action<Vector3> Drove = delegate { };
        
        [Header("Health")]
        [SerializeField]
        protected int _health = 0;

        [Header("Speed")]
        [SerializeField]
        protected float _speed = 0;
        [SerializeField]
        protected float _maxSpeed = 0;
        [SerializeField]
        protected float _speedTurn = 0;
        [SerializeField]
        protected float _brake = 0;
        [SerializeField]
        protected float _boost = 0;

        [Header("Rotation")]
        [SerializeField]
        protected float _speedRotation = 0;
        [SerializeField]
        protected float _speedReturnRotartion = 0;
        [SerializeField]
        protected float _rotationAngel = 0;

        [Header("Paint")]
        [SerializeField]
        private ColorSetting _colorSetting = null;
        [SerializeField]
        protected Renderer[] _painElements = null;

        [Header("Other")]
        [SerializeField]
        protected RoadBounds _roadBounds = null;

        private Vector3 _nextPosition = Vector3.zero;
        private Vector3 _dragPosition = Vector3.zero;
        
        private float _startSpeed = 0;

        public float Speed => _speed;
        public float MaxSpeed => _maxSpeed;
        public int Health => _health;
        public ColorSetting ColorSetting => _colorSetting;

        protected void Awake()
        {
            _startSpeed = _speed;
            _dragPosition = transform.position;
        }

        protected void OnEnable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
            Score.Boost += Score_Boost;
        }

        protected void OnDisable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
            Score.Boost -= Score_Boost;
        }

        protected void Update()
        {
            MoveForward();
            SetTurn();

            Drove(transform.position);
        }

        public virtual void GetDamage()
        {
            _health--;

            Vibration.Play();
            
            if (_health <= 0)
            {
                OnDie();
            }
        }

        public void OnDie()
        {
            _speed = 0;
            _startSpeed = 0;
            _speedTurn = 0;
            
            Died(transform.position);
        }
        
        protected void SetColor(string key)
        {
            var numberColor = PlayerPrefs.GetInt(key);
            var newMaterial = _colorSetting.SelectMaterial((Colors)numberColor);

            for (var i = 0; i < _painElements.Length; i++)
            {
                _painElements[i].sharedMaterial = newMaterial;
            }
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

        protected void Brake()
        {
            _speed -= _brake * Time.deltaTime;
        }

        private void SwipeController_Dragged(Vector3 dragPositionVector3)
        {
            _nextPosition.x = _dragPosition.x + dragPositionVector3.x * (_speedTurn * Time.deltaTime);
        }

        private void SetStartSpeed()
        {
            if (_speed < _startSpeed)
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
                _speedReturnRotartion * Time.deltaTime);
        }
        
        private void Score_Boost(float boost)
        {
            if (_speed <= _maxSpeed)
            {
                _startSpeed += _boost;
            }
        }
    }
}