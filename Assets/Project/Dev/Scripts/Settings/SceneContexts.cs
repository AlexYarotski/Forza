using UnityEngine;

public class SceneContexts : Singleton<SceneContexts>
{
    [field: SerializeField] 
    public CarDataSetting CarDataSetting 
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

    [field: SerializeField]
    public SceneWindowSetting SceneWindowSetting
    {
        get;
        private set;
    }
    
    [field: SerializeField]
    public AudioManagerSetting AudioManagerSetting
    {
        get;
        private set;
    }
}

