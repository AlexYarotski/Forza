using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance
    {
        get; 
        private set;
    }
    
    private Window _window = null;

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

    public void Load(string sceneName)
    {
        if (_window != null)
        {
            _window.Hide();
        }
        
        SceneLoader.Instance.Load(sceneName);
        
        ChooseWindow(sceneName);
        
        _window.Show();
    }
    
    private void ChooseWindow(string windowName)
    {
        var windowSwitcher = WindowSwitcher.Instance;

        switch (windowName)
        {
            case "Menu" :
                _window = windowSwitcher.GetWindow<MainWindow>();
                break;
            
            case "Garage" :
                _window = null;
                break;
            
            case "Game" :
                _window = windowSwitcher.GetWindow<GameWindow>();
                break;
            
            default:
                break;
        }
    }
}
