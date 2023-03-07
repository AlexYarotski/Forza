using UnityEngine;

namespace Project.Dev.Scripts.SceneContext
{
    [CreateAssetMenu(fileName = "ScoreSetting", menuName = "Settings/ScoreSetting", order = 0)]
    public class ScoreSetting : ScriptableObject
    {
        [SerializeField]
        private float _speedBoostScore = 0;

        public float SpeedBoostScore => _speedBoostScore;
    }
}