using UnityEngine;

namespace Project.Dev.Scripts.SceneContext
{
    [CreateAssetMenu(fileName = "RoadBoundsSetting", menuName = "Settings/RoadBoundsSetting", order = 0)]
    public class RoadBoundsSetting : ScriptableObject
    {
        [SerializeField]
        private float _leftBoundAxisX = 0;

        [SerializeField]
        private float _rightBoundAxisX = 0;

        public float LeftBoundAxisX => _leftBoundAxisX;
        public float RightBoundAxisX => _rightBoundAxisX;
    }
}