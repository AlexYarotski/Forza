using System;
using DG.Tweening;
using Project.Dev.Scripts;
using Project.Dev.Scripts.Menu;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    private const string Game = "Game";
    private const string Garage = "Garage";
    private const string KeyCar = "Car";
    
    public static event Action<string> PickedScene = delegate {  }; 
    
    [SerializeField]
    private Button _garageButton = null;
    [SerializeField]
    private Button _playButton = null;
    [SerializeField]
    private Button _settingButton = null;
    [SerializeField]
    private UISetting _uiSetting = null;

    
    private void Awake()
    {
        _playButton.AddListener(PlayGame);
        _garageButton.AddListener(OpenGarage);
        _settingButton.AddListener(OpenSetting);

        _uiSetting.transform.localScale = Vector3.zero;
        
        if (!PlayerPrefs.HasKey(KeyCar))
        {
            PlayerPrefs.SetInt(KeyCar, 0);
        }
    }

    private void PlayGame()
    {
        DOTween.PauseAll();
        PickedScene(Game);
    }

    private void OpenGarage()
    {
        DOTween.PauseAll();
        PickedScene(Garage);
    }

    private void OpenSetting()
    {
        _uiSetting.gameObject.SetActive(true);
        _uiSetting.transform.DOScale(new Vector3(1, 1), 0.5f);
    }
}
