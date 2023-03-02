using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 0;
        
        [SerializeField]
        private float _speedTurn = 0;

        [SerializeField]
        private float _brake = 0;

        [SerializeField]
        private RoadBounds _roadBounds = null;

        private Vector3 _dragPosition = Vector3.zero;
        private Vector3 _nextPosition = Vector3.zero;

        private float _startSpeed = 0;
        
        private void Awake()
        {
            _startSpeed = _speed;
            _dragPosition = transform.position;
        }

        private void OnEnable()
        {
            SwipeController.Dragged += SwipeController_Dragged;
        }
        
        private void OnDisable()
        { 
            SwipeController.Dragged -= SwipeController_Dragged;
        }

        private void Update()
        {
            MovingForward();
            Turn();
        }

        private void MovingForward()
        {
            var posAxisZ = transform.position.z + _speed * Time.deltaTime;

            _nextPosition = new Vector3(_nextPosition.x, transform.position.y, posAxisZ);
        }
        
        private void SwipeController_Dragged(Vector2 dragPositionVector2)
        {
            var dragPositionVector3 = new Vector3(dragPositionVector2.x, transform.position.y, transform.position.z);
            
            _nextPosition = _dragPosition + dragPositionVector3 * (_speedTurn * Time.deltaTime);
        }

        private void Turn()
         {
            if ( _roadBounds.IsInBounds(_nextPosition))
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

        private void ReturnStartingSpeed()
        {
            if(_speed < _startSpeed)
            {
                _speed += _brake * Time.deltaTime;
            }
        }
        
        private void Brake()
        {
            _speed -= _brake * Time.deltaTime;
        }
    }
}