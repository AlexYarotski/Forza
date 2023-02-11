using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        private float _speedTurn = 0;

        [SerializeField]
        private RoadBounds _roadBounds = null;

        private Vector3 _turningPosition = Vector3.zero ;

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
        
        private void SwipeController_Dragged(Vector2 dragPositionVector2)
        {
            Turn(dragPositionVector2);
        }

        private void Turn(Vector2 dragPositionVector2)
        {
            var dragPosition = new Vector3(dragPositionVector2.x, 0, 0);
            
            if (_roadBounds.IsInBounds(_turningPosition))
            {
                transform.position = _turningPosition + dragPosition * _speedTurn * Time.deltaTime;
            }
            else
            {
                TurnToBorder(dragPosition);
            }
            
            _turningPosition = transform.position;
        }

        private void TurnToBorder(Vector3 dragPosition)
        {
            if (dragPosition.x > 0)
            {
                if (CanDragToRight())
                {
                    transform.position = _turningPosition + dragPosition * _speedTurn * Time.deltaTime;
                }
            }
            else
            {
                if (CanDragToLeft())
                {
                    transform.position = _turningPosition + dragPosition * _speedTurn * Time.deltaTime;
                }
            }
        }

        private bool CanDragToLeft()
        {
            return !_roadBounds.IsLeftBound;
        }
        
        private bool CanDragToRight()
        {
            return !_roadBounds.IsRightBound;
        }
    }
}