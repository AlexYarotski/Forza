using System;
using System.Collections;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Urus : Car
    {
        public static event Action<float> Drove = delegate { };
        public static event Action<float> Died = delegate { };
        
        [SerializeField]
        private RoadBounds _roadBounds = null;

        private Vector3 _dragPosition = Vector3.zero;
        private Vector3 _nextPosition = Vector3.zero;
        
        private Renderer[] _renderer = null;
        
        private void Awake()
        {
            _renderer = GetComponentsInChildren<Renderer>();
        
            // var setting = SceneContexts.SceneContexts.Instance.UrusSetting;
            //
            // _speed = setting.Speed;
            // _speedTurn = setting.SpeedTurn;
            // _boost = setting.Boost;
            // _brake = setting.Brake;
            // _roadBounds = setting.RoadBounds;
            
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
            MovingForward();
            Turn();

            Drove(transform.position.z);
        }

        private void SwipeController_Dragged(Vector2 dragPositionVector2)
        {
            var dragPositionVector3 = new Vector3(dragPositionVector2.x, transform.position.y, transform.position.z);

            _nextPosition = _dragPosition + dragPositionVector3 * (_speedTurn * Time.deltaTime);
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
            Brake();
            
            TakingHealth();
            
            StartCoroutine(BecomeImmortality());
        }

        private void TakingHealth()
        {
            _health--;
            
            _smoke.Play();
            
            if (_health <= 0)
            {
                Dead();
            }
        }
        
        protected override void Dead()
        {
            base.Dead();

            Died(transform.position.z);
        }

        private IEnumerator BecomeImmortality()
        {
            var timeImmortality = new WaitForSeconds(_timeOfImmortality);
            var startColor = new Color[_renderer.Length];

            GetComponent<BoxCollider>().enabled = false;
            
            for (int i = 0; i < _renderer.Length; i++)
            {
                startColor[i] = _renderer[i].material.color;
                _renderer[i].material.color = Color.red;
            }

            yield return timeImmortality;

            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].material.color = startColor[i];
            }
            
            GetComponent<BoxCollider>().enabled = true;
        }

        private void MovingForward()
        {
            var posAxisZ = transform.position.z + _speed * Time.deltaTime;

            _nextPosition = new Vector3(_nextPosition.x, transform.position.y, posAxisZ);
        }

        private void Turn()
        {
            if (_health <= 0)
            {
                _speed = 0;
                return;
            }
            
            if (_roadBounds.IsInBounds(_nextPosition))
            {
                transform.position = _nextPosition;
                ReturnStartingSpeed();
            }
            else
            {
                transform.position = _roadBounds.ClampPosition(_nextPosition);
                Brake();
            }

            _dragPosition = transform.position;
        }
    }
}