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

        [Header("Spring")]
        [SerializeField]
        protected float _angularFrequency = 0;
        [SerializeField]
        protected float _dampingRatio = 0;

        [Header("Other")]
        [SerializeField]
        protected RoadBounds _roadBounds = null;

        protected float _startSpeed = 0;

        private Vector3 _nextPosition = Vector3.zero;
        private Vector3 _dragPosition = Vector3.zero;

        public float Speed => _speed;
        public float MaxSpeed => _maxSpeed;

        private void Awake()
        {
            _startSpeed = _speed;
            _dragPosition = transform.position;
        }

        protected virtual void OnEnable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
        }

        protected virtual void OnDisable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
        }

        protected void Update()
        {
            if (_health > 0)
            {
                MoveForward();

                SetTurn();

                Drove(transform.position);
            }
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
            Died(transform.position);
        }

        protected void MoveForward()
        {
            var position = transform.position;
            var posAxisZ = position.z + _speed * Time.deltaTime;

            position = new Vector3(position.x, 0, posAxisZ);
            transform.position = position;
        }

        protected void SetTurn()
        {
            if (_roadBounds.IsInBounds(_nextPosition))
            {
                SetRotation();

                var position = transform.position;

                _nextPosition = new Vector3(_nextPosition.x, position.y, position.z);
                
                position = _nextPosition;
                transform.position = position;

                SetStartRotation();

                SetStartSpeed();
            }
            else
            {
                SetStartRotation();

                _nextPosition = _roadBounds.ClampPosition(_nextPosition);

                var position = transform.position;
                 _nextPosition = new Vector3(_nextPosition.x, position.y, position.z);

                position = _nextPosition;
                transform.position = position;

                Brake();
            }
            
            _dragPosition = transform.position;
        }

        protected void Brake()
        {
            _speed -= _brake * Time.deltaTime;
        }

        private void SwipeController_Dragged(Vector3 dragPositionVector3)
        {
            _nextPosition.x = _dragPosition.x + dragPositionVector3.x * (_speedTurn * Time.deltaTime);
            
            Spring.CalcDampedSpringMotionParams(ref _nextPosition, _speedTurn,
                _angularFrequency, _dampingRatio);
        }

        private void SetStartSpeed()
        {
            if (_speed < _startSpeed && _health >= 0)
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
    }
}