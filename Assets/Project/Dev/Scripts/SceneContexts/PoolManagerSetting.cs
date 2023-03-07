using UnityEngine;

namespace Project.Dev.Scripts.SceneContext
{
    [CreateAssetMenu(fileName = "PoolManagerSetting", menuName = "Settings/PoolManagerSetting", order = 0)]
    public class PoolManagerSetting : ScriptableObject
    {
        [SerializeField]
        private PoolConfig[] _poolConfigs = null;

        public PoolConfig[] PoolConfigs => _poolConfigs;
    }
}