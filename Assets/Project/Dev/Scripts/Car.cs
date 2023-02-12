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
        private Vector3 _dragPosition = Vector3.zero;

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
            Turn();
        }

        private void SwipeController_Dragged(Vector2 dragPositionVector2)
        {
            _dragPosition = new Vector3(dragPositionVector2.x, 0, 0);
        }

        private void Turn()
        {
            var nextPosition = _turningPosition + _dragPosition * (_speedTurn * Time.deltaTime);
            
            if (_roadBounds.IsInBounds(_turningPosition))
            {
                transform.position = nextPosition;
            }
            else
            {
                var positionOnBorder = _roadBounds.ClampPosition(nextPosition.x);
                
                transform.position = positionOnBorder;
            }
            
            _dragPosition = Vector3.zero;
            _turningPosition = transform.position;
        }
    }
}