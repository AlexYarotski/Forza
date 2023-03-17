using System;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour
    {
        public static event Action<float> Died = delegate { };
        
        [SerializeField]
        protected int _health = 0;
        
        [SerializeField]
        protected float _speed = 0;

        [SerializeField]
        protected float _maxSpeed = 0;

        [SerializeField]
        protected float _speedTurn = 0;

        [SerializeField]
        protected float _speedRotation = 0;

        [SerializeField]
        protected float _speedReturnPosition = 0;
        
        [SerializeField]
        protected float _rotationAngel = 0;
        
        [SerializeField]
        protected float _boost = 0;
        
        [SerializeField]
        protected float _brake = 0;
        
        [SerializeField]
        protected float _timeOfImmortality = 0;

        public float Speed => _speed;
        public float MaxSpeed => _maxSpeed;
        
        protected float _startSpeed = 0;

        protected virtual void MovingForward()
        {
            
        }

        protected virtual void Turn()
        {
            
        }
        
        protected virtual void Brake()
        {
            _speed -= _brake * Time.deltaTime;
        }
        
        protected virtual void ReturnStartingSpeed()
        {
            if (_speed < _startSpeed && _health >= 0)
            {
                _speed += _brake * Time.deltaTime;
            }
        }
        
        protected virtual void ReturnStartRotation()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity,
                _speedReturnPosition * Time.deltaTime);
        }
        
        protected virtual void TakingHealth()
        {
            _health--;

            if (_health <= 0)
            {
                Dead();
            }
        }
        
        protected virtual void Dead()
        {
            StopAllCoroutines();
            
            gameObject.SetActive(false);
            Died(transform.position.z);
        }
    }
}