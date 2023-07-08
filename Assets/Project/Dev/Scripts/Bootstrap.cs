using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private const string Menu = "Menu";

    [SerializeField]
    private MonoBehaviour[] _dotnDestroyElements = null;

    private void Start()
    {
        for (int i = 0; i < _dotnDestroyElements.Length; i++)
        {
            _dotnDestroyElements[i].transform.parent = null;
            
            DontDestroyOnLoad(_dotnDestroyElements[i]);
        }
        
        SceneLoader.Instance.Load(Menu);
    }
}
