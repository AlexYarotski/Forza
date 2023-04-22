using System;
using System.Threading.Tasks;
using Project.Dev.Scripts;
using Project.Dev.Scripts.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSpecifications : MonoBehaviour
{
    private const string MainMenu = "Menu";
    private const string Game = "Game";
    private const string KeyCar = "Car";
    private const string KeyScore = "Score";

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
    private TextMeshProUGUI _health = null;
    
    [Space(15)]
    [SerializeField]
    private float _speedDivider = 0;
    
    [Header("Slider")]
    [SerializeField]
    private Slider _sliderStartSpeed = null;
    [SerializeField]
    private Slider _sliderMaxSpeed = null;
    [SerializeField]
    private Slider _sliderHealth = null;

    [Header("Button")]
    [SerializeField]
    private Button _game = null;
    [SerializeField]
    private Button _mainMenu = null;
    [SerializeField]
    private Button _previous = null;
    [SerializeField]
    private Button _next = null;

    [Header("Score")]
    [SerializeField]
    private int _scoreSecondCar = 0;
    [SerializeField]
    private int _scoreThirdCar = 0;

    [Header("Lock")]
    [SerializeField]
    private Lock _lock = null;
    
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
        var prevCar = indexCar == 0 ? _cars[_cars.Length - 1] : _cars[indexCar - 1];
        
        _car.gameObject.SetActive(false);

        _car = CheckNewCar(prevCar);
        
        _car.gameObject.SetActive(true);
        
        SetValue();
    }

    private void NextCar()
    {
        var indexCar = Array.IndexOf(_cars ,_car);
        var nextCar = indexCar == _cars.Length - 1 ? _cars[0] : _cars[indexCar + 1];
        
        _car.gameObject.SetActive(false);
        
        _car = CheckNewCar(nextCar);
        
        _car.gameObject.SetActive(true);
        
        SetValue();
    }

    private Car CheckNewCar(Car newCar)
    {
        var bestScore = PlayerPrefs.GetInt(KeyScore);
        
        if (bestScore <= _scoreSecondCar)
        {
            _lock.gameObject.SetActive(true);
            _lock.Score(3000);
            return newCar;
        }
        
        if (bestScore >= _scoreThirdCar)
        {
            //Note: added when a new car appears
        }

        _lock.gameObject.SetActive(false);
        return newCar;
    }

    private void SetValue()
    {
        var startSpeed = (int)_car.Speed + 80;
        var maxSpeed = (int)_car.MaxSpeed + 80;
        var health = (float)_car.Health / 10;
        
        _sliderStartSpeed.value = startSpeed / _speedDivider;
        _sliderMaxSpeed.value = maxSpeed / _speedDivider;
        _sliderHealth.value = health;
        
        _name.text = _car.name;
        _startSpeed.text = Convert.ToString(startSpeed);
        _maxSpeed.text = Convert.ToString(maxSpeed);
        _health.text = Convert.ToString(_car.Health);
    }
}
