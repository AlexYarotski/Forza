using UnityEngine;

namespace Project.Dev.Scripts.SceneContext
{
    [CreateAssetMenu(fileName = "UrusSetting", menuName = "Settings/UrusSetting", order = 0)]
    public class UrusSetting : ScriptableObject
    {
        [SerializeField]
        protected float _speed = 0;
        
        [SerializeField]
        protected float _speedTurn = 0;

        [SerializeField]
        protected float _boost = 0;

        [SerializeField]
        protected float _brake = 0;
        
        [SerializeField]
        private RoadBounds _roadBounds = null;

        public float Speed => _speed;
        public float SpeedTurn => _speedTurn;
        public float Boost => _boost;
        public float Brake => _brake;
        public RoadBounds RoadBounds => _roadBounds;
    }
}