using UnityEngine;

namespace Project.Dev.Scripts.SceneContext
{
    public class SceneContext : MonoBehaviour
    {
        [field: SerializeField]
        public UrusSetting UrusSetting
        {
            get;
            private set;
        }
    
        [field: SerializeField]
        public ChunkGeneratorSetting ChunkGeneratorSetting
        {
            get;
            private set;
        }

        [field: SerializeField]
        public GameWindowSetting GameWindowSetting
        {
            get;
            private set;
        }
    
        [field: SerializeField]
        public RoadBoundsSetting RoadBoundsSetting
        {
            get;
            private set;
        }
    
        [field: SerializeField]
        public ScoreSetting ScoreSetting
        {
            get;
            private set;
        }
    
        [field: SerializeField]
        public PoolManagerSetting PoolManagerSetting
        {
            get;
            private set;
        }
    
        public static SceneContext Inctance
        {
            get; 
            private set;
        }
    
        private void Awake()
        {
            if (Inctance == null)
            {
                Inctance = this;
            }
            else if(Inctance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
