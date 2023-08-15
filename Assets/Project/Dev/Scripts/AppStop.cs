using UnityEngine.SceneManagement;

public class AppStop : DontDestroyElement
{
    private const string NameSpecialSettingsScene = "Game";
    
    private bool _isPaused = false;
    
    private void OnGUI()
    {
        if (_isPaused)
        {
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == NameSpecialSettingsScene)
            {
                WindowSwitcher.Instance.Show<GameSettingWindow>();
            }
            else
            {
                WindowSwitcher.Instance.Show<SettingWindow>();
            }

            _isPaused = false;
        }
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        _isPaused = pauseStatus;
    }
}