using System;
using Project.Dev.Scripts.Interface;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour, IDamageable
    {
        public static event Action<Vector3> Died = delegate { };
        
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
        
        [Header("Other")]
        [SerializeField]
        protected RoadBounds _roadBounds = null;
        [SerializeField]
        protected Spring _spring = null;
        
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

        protected void OnEnable()
        {
            SwipeController.Dragged +=  SwipeController_Dragged;
        }

        protected void OnDisable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
        }

        public virtual void GetDamage()
        {
            _health--;

            if (_health <= 0)
            {
                OnDie();
            }
        }
        
        protected void MoveForward()
        {
            var position = transform.position;
            var posAxisZ = position.z + _speed * Time.deltaTime;
            
            
            position = new Vector3(position.x, 0, posAxisZ);
            transform.position = position;

            _nextPosition = new Vector3(_nextPosition.x, position.y, position.z);
        }

        protected void SetTurn()
        {
            if (_roadBounds.IsInBounds(_nextPosition))
            {
                SetRotation();

                var position = transform.position;
                var nextPositionX = new Vector3(_nextPosition.x, position.y, position.z);
                
                position =  nextPositionX;
                transform.position = position;

                SetStartRotation();
                
                SetStartSpeed();
            }
            else
            {
                SetStartRotation();
                
                _nextPosition = _roadBounds.ClampPosition(_nextPosition);

                var position = transform.position;
                var nextPositionX = new Vector3(_nextPosition.x, position.y, position.z);
                
                position = nextPositionX;
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

        private void OnDie()
        {
            Died(transform.position);
        }
    }
}