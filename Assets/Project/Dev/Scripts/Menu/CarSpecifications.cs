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
    private const string KeyCar = "Car";

    [SerializeField]
    private Car[] _cars = null;

    [Header("TMP")]
    [SerializeField]
    private TextMeshProUGUI _name = null;
    [SerializeField]
    private TextMeshProUGUI _startSpeed = null;
    [SerializeField]
    private TextMeshProUGUI _maxSpeed = null;
    [SerializeField]
    private float _speedDivider = 0;
    
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
    [SerializeField]
    private Button _previous = null;
    [SerializeField]
    private Button _next = null;

    private Car _car = null;
    
    private void Awake()
    {
        _car = Search.ActiveCar(_cars);
        
        _game.onClick.AddListener(StartGame);
        _mainMenu.onClick.AddListener(Menu);
        _previous.onClick.AddListener(PreviousCar);
        _next.onClick.AddListener(NextCar);
        
        SetValue();
    }

    private async void UploadSceneAsync(string sceneName)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }
    
    private void Menu()
    {
        PlayerPrefs.SetString(KeyCar, $"{_car.GetType()}");
        
        UploadSceneAsync(MainMenu);
    }
    
    private void StartGame()
    {
        PlayerPrefs.SetString(KeyCar, $"{_car.GetType()}");
        
        UploadSceneAsync(Game);
    }

    private void PreviousCar()
    {
        var indexCar = Array.IndexOf(_cars ,_car);

        _car.gameObject.SetActive(false);
        
        _car = indexCar == 0 ? _cars[_cars.Length - 1] : _cars[indexCar - 1];
        
        _car.gameObject.SetActive(true);
        
        SetValue();
    }

    private void NextCar()
    {
        var indexCar = Array.IndexOf(_cars ,_car);

        _car.gameObject.SetActive(false);
        
        _car = indexCar == _cars.Length - 1 ? _cars[0] : _cars[indexCar + 1];
        
        _car.gameObject.SetActive(true);
        
        SetValue();
    }

    private void SetValue()
    {
        var startSpeed = _car.Speed / _speedDivider;
        var maxSpeed = _car.MaxSpeed / _speedDivider;
        
        _sliderStartSpeed.value = startSpeed;
        _sliderMaxSpeed.value = maxSpeed;
        
        _name.text = _car.name;
        _startSpeed.text = Convert.ToString(_car.Speed);
        _maxSpeed.text = Convert.ToString(_car.MaxSpeed);
    }
}
