using UnityEngine;

public class SceneContexts : MonoBehaviour
{
    public static SceneContexts Instance
    {
        get;
        private set;
    }
    
    [field: SerializeField] 
    public CarDataSettings CarDataSettings 
    {
        get; 
        private set; 
    }

    [field: SerializeField]
    public LockCarSetting LockCarSetting
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
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }
}

