using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour
    {
        [SerializeField]
        protected int _health = 0;
        
        [SerializeField]
        protected float _speed = 0;

        [SerializeField]
        protected float _maxSpeed = 0;

        [SerializeField]
        protected float _speedTurn = 0;
        
        [SerializeField]
        protected float _boost = 0;
        
        [SerializeField]
        protected float _brake = 0;
        
        [SerializeField]
        protected float _timeOfImmortality = 0;
        
        [SerializeField]
        protected float _distanceToDangerousOvertaking = 0;

        [SerializeField]
        protected Barrier _barrier = null;

        protected float _startSpeed = 0;

        protected virtual void MovingForward()
        {
            
        }

        protected virtual void Turn()
        {
            
        }
        
        protected void ReturnStartingSpeed()
        {
            if (_speed < _startSpeed && _health >= 0)
            {
                _speed += _brake * Time.deltaTime;
            }
        }

        protected void Brake()
        {
            _speed -= _brake * Time.deltaTime;
        }
        
        protected virtual void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}