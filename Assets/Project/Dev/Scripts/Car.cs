using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        private float _speedTurn = 0;

        [SerializeField]
        private RoadBounds _roadBounds = null;

        private Vector3 _turningPosition = Vector3.zero;
        private Vector3 _nextPosition = Vector3.zero;

        private void Awake()
        {
            _turningPosition = transform.position;
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
            var nextPositionAxisX = Mathf.Clamp(_nextPosition.x, _roadBounds.LeftBoundAxisX, 
                _roadBounds.RightBoundAxisX);
                
            transform.position = new Vector3(nextPositionAxisX, 0, 0);
        }

        private void SwipeController_Dragged(Vector2 dragPositionVector2)
        {
            Turn(dragPositionVector2);
        }

        private void Turn(Vector2 dragPositionVector2)
        {
            var dragPosition = new Vector3(dragPositionVector2.x, 0, 0);
            
            _nextPosition = _turningPosition + dragPosition * _speedTurn * Time.deltaTime;

            if (_roadBounds.IsInBounds(_turningPosition))
            {
                transform.position = _nextPosition;
            }
            
            _turningPosition = transform.position;
        }
    }
}