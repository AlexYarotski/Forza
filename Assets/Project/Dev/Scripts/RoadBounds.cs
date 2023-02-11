using UnityEngine;

namespace Project.Dev.Scripts
{
    public class RoadBounds : MonoBehaviour
    {
        [SerializeField]
        private float _leftBound = 0;

        [SerializeField]
        private float _rightBound = 0;

        public bool IsLeftBound
        {
            get;
            private set;
        }

        public bool IsRightBound
        {
            get;
            private set;
        }

        private void Awake()
        {
            CheckingBoundaries();
        }

        public bool IsInBounds(Vector3 position)
        {
            var positionAxisX = position.x;

            if (positionAxisX >= _rightBound)
            {
                IsRightBound = true;

                return false;
            }

            if (positionAxisX <= _leftBound)
            {
                IsLeftBound = true;

                return false;
            }

            IsRightBound = false;
            IsLeftBound = false;

            return true;
        }

        private void CheckingBoundaries()
        {
            if (_leftBound == _rightBound)
            {
                Debug.LogError("The borders cannot be equal!");
            }

            if (_leftBound > _rightBound)
            {
                Debug.LogError("The left border cannot be greater than the right!");
            }
        }
    }
}