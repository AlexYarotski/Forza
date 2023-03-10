using UnityEngine;

namespace Project.Dev.Scripts
{
    public class RoadBounds : MonoBehaviour
    {
        [SerializeField]
        private float _leftBoundAxisX = 0;
        
        [SerializeField]
        private float _rightBoundAxisX = 0;

        private void Awake()
        {
            // var setting = SceneContexts.SceneContexts.Instance.RoadBoundsSetting;
            //
            // _leftBoundAxisX = setting.LeftBoundAxisX;
            // _rightBoundAxisX = setting.RightBoundAxisX;
            
            CheckingBoundaries();
        }

        public bool IsInBounds(Vector3 position)
        {
            var positionAxisX = position.x;

            return positionAxisX <= _rightBoundAxisX && positionAxisX >= _leftBoundAxisX;
        }

        public Vector3 ClampPosition(Vector3 position)
        {
            var nextPositionAxisX = Mathf.Clamp(position.x, _leftBoundAxisX, 
                _rightBoundAxisX);

            return new Vector3(nextPositionAxisX, position.y, position.z);
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