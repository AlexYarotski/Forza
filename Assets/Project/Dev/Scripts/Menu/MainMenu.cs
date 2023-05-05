using System;
using Project.Dev.Scripts.Menu;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private const string Game = "Game";
    private const string Garage = "Garage";
    
    public static event Action<string> PickedScene = delegate {  }; 
    
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
        _setting.gameObject.SetActive(true);
    }
}
