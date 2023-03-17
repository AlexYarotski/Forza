using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Urus : Car
    {
        public static event Action<Vector3> Drove = delegate { };
        
        [SerializeField]
        private RoadBounds _roadBounds = null;

        private Vector3 _dragPosition = Vector3.zero;
        private Vector3 _nextPosition = Vector3.zero;
        
        private Renderer[] _renderer = null;
        private bool _isTurn = false;

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

            if (_isTurn)
            {
                Turn();
            }
            else
            {
                ReturnStartRotation();
            }
            
            ReturnStartingSpeed();
            
            Drove(transform.position);
        }

        protected override void MovingForward()
        {
            base.MovingForward();

            var posAxisZ = transform.position.z + _speed * Time.deltaTime;

            _nextPosition = new Vector3(_nextPosition.x, transform.position.y, posAxisZ);

            transform.position = new Vector3(transform.position.x, transform.position.y, _nextPosition.z);
        }

        protected override void Turn()
        {
            base.Turn();

            if (_roadBounds.IsInBounds(_nextPosition))
            {
                Rotation();
                
                transform.position = Vector3.Lerp(transform.position ,_nextPosition, 1);
                
                ReturnStartRotation();
            }
            else
            {
                ReturnStartRotation();

                _nextPosition = _roadBounds.ClampPosition(_nextPosition);
                
                transform.position = Vector3.Lerp(transform.position, _nextPosition, 1);
                
                Brake();
            }

            _isTurn = false;
            _dragPosition = transform.position;
        }

        private void SwipeController_Dragged(Vector3 dragPositionVector2)
        {
            var dragPositionVector3 = new Vector3(dragPositionVector2.x, 0, transform.position.z);

            _nextPosition.x =  _dragPosition.x + dragPositionVector3.x * (_speedTurn * Time.deltaTime);

            _isTurn = true;
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
            
            StartCoroutine(BecomeImmortality());
            
            TakingHealth();
        }

        private void Rotation()
        {
            var nextRotation = Quaternion.identity;

            if (_nextPosition.x < transform.position.x)
            {
                nextRotation.eulerAngles = new Vector3(transform.rotation.x, -_rotationAngel,
                    transform.rotation.z);
            }
            else if (_nextPosition.x > transform.position.x)
            {
                nextRotation.eulerAngles = new Vector3(transform.rotation.x, _rotationAngel,
                    transform.rotation.z);
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, _speedRotation * Time.deltaTime);
        }

        private IEnumerator BecomeImmortality()
        {
            var timeImmortality = new WaitForSeconds(_timeOfImmortality);
            var boxCollider =  GetComponent<BoxCollider>();
            var startColor = new Color[_renderer.Length];
            
            boxCollider.enabled = false;
            
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
            
            boxCollider.enabled = true;
        }
    }
}