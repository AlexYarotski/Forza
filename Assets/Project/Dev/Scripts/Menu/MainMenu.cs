using System.Threading.Tasks;
using Project.Dev.Scripts.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const string Game = "Game";
    private const string Garage = "Garage";
    
    [SerializeField]
    private Button _garageButton = null;
    [SerializeField]
    private Button _playButton = null;
    [SerializeField]
    private Button _settingButton = null;

    [SerializeField]
    private Setting _setting = null;
    
    private void Awake()
    {
        _playButton.onClick.AddListener(PlayGame);
        _garageButton.onClick.AddListener(Cancel);
        _settingButton.onClick.AddListener(Setting);
    }

    private void FixedUpdate()
    {
        if (!_setting.gameObject.activeSelf)
        {
            SetComponentsActive(true);
        }
    }

    private void PlayGame()
    {
        UploadSceneAsync(Game);
    }

    private void Cancel()
    {
        UploadSceneAsync(Garage);
    }

    private void Setting()
    {
        _setting.gameObject.SetActive(true);

        SetComponentsActive(false);
    }
    
    private async void UploadSceneAsync(string sceneName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }

    private void SetComponentsActive(bool isActive)
    {
        _playButton.gameObject.SetActive(isActive);
        _garageButton.gameObject.SetActive(isActive);
        _settingButton.gameObject.SetActive(isActive);
    }
}
