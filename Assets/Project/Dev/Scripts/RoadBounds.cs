using UnityEngine;

namespace Project.Dev.Scripts
{
    public class RoadBounds : MonoBehaviour
    {
        [SerializeField]
        private float _leftBoundAxisX = 0;

        [SerializeField]
        private float _rightBoundAxisX = 0;
        
        public bool IsInBounds(Transform transform)
        {
            CheckingBoundaries();

            var position = transform.position;
            var positionAxisX = position.x;

            if (positionAxisX < _leftBoundAxisX)
            {
                return false;
            }

            if (positionAxisX > _rightBoundAxisX)
            {
                return false;
            }

            return true;
        }

        private void CheckingBoundaries()
        {
            if (_leftBoundAxisX == _rightBoundAxisX)
            {
                Debug.LogError("The borders cannot be equal!");
            }

            if (_leftBoundAxisX > _rightBoundAxisX)
            {
                Debug.LogError("The left border cannot be greater than the right!");
            }
        }
    }
}