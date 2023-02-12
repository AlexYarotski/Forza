using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        private float _speedTurn = 0;

        [SerializeField]
        private RoadBounds _roadBounds = null;

        private Vector3 _dragPosition = Vector3.zero;
        private Vector3 _nextPosition = Vector3.zero;

        private void Awake()
        {
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
            Turn();
        }

        private void SwipeController_Dragged(Vector2 dragPositionVector2)
        {
            var dragPositionVector3 = new Vector3(dragPositionVector2.x, 0, 0);
            
            _nextPosition = _dragPosition + dragPositionVector3 * (_speedTurn * Time.deltaTime);
        }

        private void Turn()
        {
            transform.position = _roadBounds.IsInBounds(_nextPosition) ? _nextPosition : _roadBounds.ClampPosition(_nextPosition.x);

            _dragPosition = transform.position;
        }
    }
}