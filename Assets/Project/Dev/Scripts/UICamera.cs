using UnityEngine;

public class UICamera : MonoBehaviour
{
    [SerializeField]
    private Camera _uiCamera = null;
    
    public static Camera Instance
    {
        get;
        private set;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = _uiCamera;
        }
        else if(Instance == this)
        {
            Destroy(gameObject);
        }
    }
}
