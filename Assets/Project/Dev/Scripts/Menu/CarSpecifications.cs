using System;
using System.Threading.Tasks;
using Project.Dev.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSpecifications : MonoBehaviour
{
    private const string MainMenu = "Menu";
    private const string Game = "Game";
    
    [SerializeField]
    private Car _car = null;

    [Header("TMP")]
    [SerializeField]
    private TextMeshProUGUI _name = null;
    [SerializeField]
    private TextMeshProUGUI _startSpeed = null;
    [SerializeField]
    private TextMeshProUGUI _maxSpeed = null;
    
    [Header("Slider")]
    [SerializeField]
    private Slider _sliderStartSpeed = null;
    [SerializeField]
    private Slider _sliderMaxSpeed = null;

    [Header("Button")]
    [SerializeField]
    private Button _game = null;
    [SerializeField]
    private Button _mainMenu = null;

    private void Awake()
    {
        _game.onClick.AddListener(StartGame);
        _mainMenu.onClick.AddListener(Mainmenu);

        SetSliderValue();
        
        _name.text = _car.name;
        _startSpeed.text = Convert.ToString(_car.Speed);
        _maxSpeed.text = Convert.ToString(_car.MaxSpeed);
    }

    private void Mainmenu()
    {
       UploadSceneAsync(MainMenu);
    }
    
    private void StartGame()
    {
        UploadSceneAsync(Game);
    }

    private void SetSliderValue()
    {
        var startSpeed = _car.Speed / 100;
        var maxSpeed = _car.MaxSpeed / 100;
        
        _sliderStartSpeed.value = startSpeed;
        _sliderMaxSpeed.value = maxSpeed;
    }
    
    private async void UploadSceneAsync(string sceneName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }
}
