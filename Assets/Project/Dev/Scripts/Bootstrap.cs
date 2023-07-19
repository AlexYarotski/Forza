using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private const string Menu = "Menu";

    [SerializeField]
    private MonoBehaviour[] _dotnDestroyElements = null;

    [SerializeField]
    private WindowManager _windowManager = null;

    private void Start()
    {
        for (int i = 0; i < _dotnDestroyElements.Length; i++)
        {
            _dotnDestroyElements[i].transform.SetParent(null);
            
            DontDestroyOnLoad(_dotnDestroyElements[i]);
        }
        
        _windowManager.Load(Menu);
    }
}
