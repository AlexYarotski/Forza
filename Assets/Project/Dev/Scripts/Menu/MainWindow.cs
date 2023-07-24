using UnityEngine;
using UnityEngine.UI;

public class MainWindow : Window
{
    private const string KeyCar = "Car";

    [SerializeField]
    private Button _garageButton = null;
    [SerializeField]
    private Button _playButton = null;
    [SerializeField]
    private Button _settingButton = null;
    
    public override bool IsPopUp
    {
        get => false;
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        
        _playButton.AddListener(PlayGame);
        _garageButton.AddListener(OpenGarage);
        _settingButton.AddListener(OpenSetting);
     
        if (!PlayerPrefs.HasKey(KeyCar))
        {
            PlayerPrefs.SetInt(KeyCar, 0);
        }
    }

    private void PlayGame()
    {
        SceneLoader.Instance.LoadGame();
    }

    private void OpenGarage()
    {
        SceneLoader.Instance.LoadGarage();
    }
    
    private void OpenSetting()
    {
        WindowSwitcher.Instance.Show<SettingWindow>();
    }
}