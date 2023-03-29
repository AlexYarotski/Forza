using System;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour
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
        protected Immortal _immortal = null;
        
        [SerializeField]
        protected Spring _spring = null;

        public float Speed => _speed;
        public float MaxSpeed => _maxSpeed;
        
        protected float _startSpeed = 0;
        
        protected Vector3 _nextPosition = Vector3.zero;
        protected Vector3 _dragPosition = Vector3.zero;

        protected void MoveForward()
        {
            var posAxisZ = transform.position.z + _speed * Time.deltaTime;

            transform.position = new Vector3(transform.position.x, 0, posAxisZ);
            
            _nextPosition = new Vector3(_nextPosition.x, transform.position.y, transform.position.z);
        }

        protected void SetTurn()
        {
            if (_roadBounds.IsInBounds(_nextPosition))
            {
                SetRotation();
                
                var nextPositionX = new Vector3(_nextPosition.x, transform.position.y, transform.position.z);
                
                transform.position = nextPositionX;
                
                SetStartRotation();
                
                SetStartSpeed();
            }
            else
            {
                SetStartRotation();
                
                _nextPosition = _roadBounds.ClampPosition(_nextPosition);
                
                var nextPositionX = new Vector3(_nextPosition.x, transform.position.y, transform.position.z);
                
                transform.position = nextPositionX;
                
                Brake();
            }
            
            _dragPosition = transform.position;
        }
        
        protected void Brake()
        {
            _speed -= _brake * Time.deltaTime;
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

        protected void GetDamage()
        {
            _health--;

            if (_health <= 0)
            {
                OnDie();
            }
        }
        
        private void OnDie()
        {
            Died(transform.position);
        }
    }
}