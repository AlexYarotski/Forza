using UnityEngine;

namespace Project.Dev.Scripts.SceneContext
{
    [CreateAssetMenu(fileName = "GameWindowSetting", menuName = "Settings/GameWindowSetting", order = 0)]
    public class GameWindowSetting : ScriptableObject
    {
        [SerializeField]
        private Color _color = Color.white;

        [SerializeField]
        private float _sizeOfIncreaseScore = 0;

        [SerializeField]
        private int _timeDelayScore = 0;

        public Color Color => _color;
        public float SizeOfIncreaseScore => _sizeOfIncreaseScore;
        public int TimeDelayScore => _timeDelayScore;
    }
}