using UnityEngine;

public class Bootstrap : DontDestroyElement
{
    [SerializeField]
    private DontDestroyElement[] _dotnDestroyElements = null;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        
        for (int i = 0; i < _dotnDestroyElements.Length; i++)
        {
            _dotnDestroyElements[i].transform.SetParent(null);
            
            DontDestroyOnLoad(_dotnDestroyElements[i]);
        }
        
        SceneLoader.Instance.LoadMain();
    }
}
