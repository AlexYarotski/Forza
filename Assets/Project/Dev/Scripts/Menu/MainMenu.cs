using System.Threading.Tasks;
using Project.Dev.Scripts.Interface;
using Project.Dev.Scripts.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IEnableButtons
{
    [SerializeField]
    private Button _garageButton = null;
    
    [SerializeField]
    private Button _playButton = null;
    
    [SerializeField]
    private Button _settingButton = null;
    
    [SerializeField]
    private Image _background = null;

    [SerializeField]
    private Setting _setting = null;
    
    private void Awake()
    {
        _setting.SetChildrenActiveState(false);
        
        _playButton.onClick.AddListener(PlayGame);
        _garageButton.onClick.AddListener(Cancel);
        _settingButton.onClick.AddListener(Setting);
    }

    private void FixedUpdate()
    {
        if (!_setting.IsActive)
        {
            EnableButtons(true);
        }
    }

    private async void PlayGame()
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync("Game");

        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }

    private async void Cancel()
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync("Garage");

        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }

    private void Setting()
    {
        EnableButtons(false);
        _setting.SetChildrenActiveState(true);
    }

    public void EnableButtons(bool enable)
    {
        _garageButton.gameObject.SetActive(enable);
        _playButton.gameObject.SetActive(enable);
        _settingButton.gameObject.SetActive(enable);
    }
}
