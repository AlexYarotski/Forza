using Project.Dev.Scripts.SceneContext;
using UnityEngine;

namespace Project.Dev.Scripts.SceneContexts
{
    public class SceneContexts : MonoBehaviour
    {
        [field: SerializeField]
        public AudiSetting AudiSetting
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

        [field: SerializeField]
        public ColorSetting ColorSetting
        {
            get;
            private set;
        }
        
        public static SceneContexts Instance
        {
            get; 
            private set;
        }
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
