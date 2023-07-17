using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : Window
{
    private const string Game = "Game";
    private const string Garage = "Garage";
    private const string KeyCar = "Car";

    [SerializeField]
    private Button _garageButton = null;
    [SerializeField]
    private Button _playButton = null;
    [SerializeField]
    private Button _settingButton = null;
    
    [SerializeField]
    private SettingWindow settingWindow = null;
    
    private SceneLoader _sceneLoader = null;

    private void Start()
    {
        _playButton.AddListener(PlayGame);
        _garageButton.AddListener(OpenGarage);
        _settingButton.AddListener(OpenSetting);
        
        _sceneLoader = SceneLoader.Instance;

        if (!PlayerPrefs.HasKey(KeyCar))
        {
            PlayerPrefs.SetInt(KeyCar, 0);
        }
    }

    private void PlayGame()
    {
        DOTween.PauseAll();
        _sceneLoader.Load(Game);
    }

    private void OpenGarage()
    {
        DOTween.PauseAll();
        _sceneLoader.Load(Garage);
    }

    private void OpenSetting()
    {
        if (!settingWindow.gameObject.activeSelf)
        {
            settingWindow.gameObject.SetActive(true);
            settingWindow.transform.DOScale(new Vector3(SettingWindow.SizeWindow, SettingWindow.SizeWindow),
                SettingWindow.OpenDuration);
        }
    }
}