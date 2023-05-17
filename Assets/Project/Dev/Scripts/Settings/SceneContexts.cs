using UnityEngine;

namespace Project.Dev.Scripts
{
    public class SceneContexts : MonoBehaviour
    {
        [field: SerializeField]
        public CarDataSettings CarDataSettings
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
