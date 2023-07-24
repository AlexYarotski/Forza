using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    { 
        get;
        protected set; 
    }

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
    }
}
