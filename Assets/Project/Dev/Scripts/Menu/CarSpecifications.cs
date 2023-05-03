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
    public static event Action NextCar = delegate { };
    public static event Action PreviousCar = delegate {  };

    private const string MainMenu = "Menu";
    private const string Game = "Game";
    private const string KeyCar = "Car";

    [SerializeField]
    private Car _startCar = null; 
    
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

    private Car _car = null;
    
    private void OnEnable()
    {
        Podium.PickedCar += Podium_PickedCar;
    }

    private void OnDisable()
    {
        Podium.PickedCar -= Podium_PickedCar;
    }

    private void Start()
    {
        _game.onClick.AddListener(StartGame);
        _mainMenu.onClick.AddListener(Menu);
        _previous.onClick.AddListener(Previous);
        _next.onClick.AddListener(Next);

        _car = _startCar;
        
        SetValue(_startCar);
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

    private void Previous()
    {
        PreviousCar();
    }

    private void Next()
    {
        NextCar();
    }
    
    private void SetValue(Car car)
    {
        var startSpeed = (int)car.Speed + 80;
        var maxSpeed = (int)car.MaxSpeed + 80;
        var health = (float)car.Health / 10;
        
        _sliderStartSpeed.value = startSpeed / _speedDivider;
        _sliderMaxSpeed.value = maxSpeed / _speedDivider;
        _sliderHealth.value = health;
        
        _name.text = car.name;
        _startSpeed.text = Convert.ToString(startSpeed);
        _maxSpeed.text = Convert.ToString(maxSpeed);
        _health.text = Convert.ToString(car.Health);
    }
    
    private void Podium_PickedCar(Car car)
    {
        SetValue(car);

        _car = car;
    }
}