using UnityEngine.SceneManagement;

public class AppStop : DontDestroyElement
{
    private const string NameSpecialSettingsScene = "Game";

    private void OnApplicationPause(bool pauseStatus)
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
    }
}