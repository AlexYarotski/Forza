using System;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Urus : Car
    {
        public static event Action<Vector3> Drove = delegate { };

        private Renderer[] _renderer = null;
        
        private ParticleManager _particleManager = null;
        private ParticleSystem _particleSmoke = null;

        private void Awake()
        {
            _renderer = GetComponentsInChildren<Renderer>();

            _startSpeed = _speed;
            _dragPosition = transform.position;
        }

        private void OnEnable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
            Score.Boost += Score_Boost;
            Barrier.Hit += Barrier_Hit;
        }
        
        private void OnDisable()
        {
            SwipeController.Dragged -= SwipeController_Dragged;
            Score.Boost -= Score_Boost;
            Barrier.Hit -= Barrier_Hit;
        }

        private void Update()
        {
            if (_health > 0)
            {
                MoveForward();
            
                SetTurn();

                Drove(transform.position);
            }
        }

        private void SwipeController_Dragged(Vector3 dragPositionVector3)
        {
            _nextPosition.x = _dragPosition.x + dragPositionVector3.x * (_speedTurn * Time.deltaTime);
        }

        private void Score_Boost(float boost)
        {
            if (_speed <= _maxSpeed)
            {
                _startSpeed += _boost;
            }
        }
        
        private void Barrier_Hit(Vector3 position)
        {
            GetDamage();
            
            if (_health <= 0)
            {
              return;   
            }
            
            Brake();
            
            StartCoroutine(_immortal.MakeImmortal(_renderer));
        }
    }
}