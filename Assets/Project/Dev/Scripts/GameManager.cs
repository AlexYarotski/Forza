using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private WindowSwitcher _windowSwitcher = null;

    public void Load(string sceneName)
    {
        SceneLoader.Instance.Load(sceneName);
        OpenUI(sceneName);
    }
    
    private void OpenUI(string sceneName)
    {
        switch (sceneName)
        {
            case "Main":
                _windowSwitcher.Show<MainWindow>();
                break;
            
            case "Garage":
                
                break;
            
            case "Game": 
                _windowSwitcher.Show<GameWindow>();
                break;
            
            default:
                Debug.Log("This window does not exist");
                break;
        }
    }
}