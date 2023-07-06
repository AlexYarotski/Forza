using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
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
    private UISetting _uiSetting = null;
    
    private SceneLoader _sceneLoader = null;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

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
        if (!_uiSetting.gameObject.activeSelf)
        {
            _uiSetting.gameObject.SetActive(true);
            _uiSetting.transform.DOScale(new Vector3(UISetting.SizeWindow, UISetting.SizeWindow),
                UISetting.OpenDuration);
        }
    }
}
