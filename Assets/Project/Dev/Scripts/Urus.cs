using System;
using System.Collections;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Urus : Car
    {
        public static event Action<Vector3> Drove = delegate { };
        
        private readonly Vector3 DistanceToHood = new Vector3(0, 0.7f, 0.7f);
        
        [Header("Other")]
        [SerializeField]
        private RoadBounds _roadBounds = null;

        [SerializeField]
        private Spring _spring = null;

        private Vector3 _dragPosition = Vector3.zero;
        private Vector3 _nextPosition = Vector3.zero;
        
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
            MovingForward();
            
            SetTurn();

            Drove(transform.position);
        }

        protected override void MovingForward()
        {
            base.MovingForward();

            var posAxisZ = transform.position.z + _speed * Time.deltaTime;

            transform.position = new Vector3(transform.position.x, 0, posAxisZ);
            
            _nextPosition = new Vector3(_nextPosition.x, transform.position.y, posAxisZ);

            if (_particleSmoke != null)
            {
                _particleSmoke.transform.position = _nextPosition + DistanceToHood;
            }
        }

        protected override void SetTurn()
        {
            base.SetTurn();
            
            if (_roadBounds.IsInBounds(_nextPosition))
            {
                SetRotation();
                
                transform.position = Vector3.Lerp(transform.position ,_nextPosition, 1);
                
                SetStartRotation();
                
                SetStartingSpeed();
            }
            else
            {
                SetStartRotation();
                
                _nextPosition = _roadBounds.ClampPosition(_nextPosition);
                
                transform.position = Vector3.Lerp(transform.position, _nextPosition, 1);
                
                Brake();
            }
            
            _dragPosition = transform.position;
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
            Brake();

            StartCoroutine(MakeImmortal());
            
            PlaySmoke();
            
            TakeHealth();
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

        private void PlaySmoke()
        {
            _particleSmoke = ParticleManager.Instance.Emit(ParticleType.CarSmoke, transform.position);
        }
        
        private IEnumerator MakeImmortal()
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