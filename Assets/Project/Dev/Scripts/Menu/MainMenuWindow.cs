using System;
using Project.Dev.Scripts.Menu;
using UnityEngine;
using UnityEngine.Serialization;
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

    [FormerlySerializedAs("_setting")]
    [SerializeField]
    private UISetting uiSetting = null;
    
    private void Awake()
    {
        _playButton.onClick.AddListener(PlayGame);
        _garageButton.onClick.AddListener(Cancel);
        _settingButton.onClick.AddListener(Setting);

        if (!PlayerPrefs.HasKey(KeyCar))
        {
            PlayerPrefs.SetInt(KeyCar, 0);
        }
    }

    private void PlayGame()
    {
        PickedScene(Game);
    }

    private void Cancel()
    {
        PickedScene(Garage);
    }

    private void Setting()
    {
        uiSetting.gameObject.SetActive(true);
    }
}
