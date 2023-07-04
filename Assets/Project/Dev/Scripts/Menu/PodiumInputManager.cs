using System;
using UnityEngine;
using UnityEngine.UI;

public class PodiumInputManager : MonoBehaviour
{
    private const string Game = "Game";
    private const string MainMenu = "Menu";
    
    public static event Action NextCar = delegate { };
    public static event Action PreviousCar = delegate {  };

    [Header("Button")]
    [SerializeField]
    private Button _gameButton = null;
    [SerializeField]
    private Button _mainMenuButton = null;
    [SerializeField]
    private Button _previousButton = null;
    [SerializeField]
    private Button _nextButton = null;
    
    private SceneLoader _sceneLoader = null;

    private void Start()
    {
        _gameButton.AddListener(StartGame);
        _mainMenuButton.AddListener(OpenMenu);
        _previousButton.AddListener(SetPreviousCar);
        _nextButton.AddListener(SetNextCar);

        _sceneLoader = SceneLoader.Instance;
    }

    private void OpenMenu()
    {
        _sceneLoader.Load(MainMenu);
    }
    
    private void StartGame()
    {
        _sceneLoader.Load(Game);
    }

    private void SetPreviousCar()
    {
        PreviousCar();
    }

    private void SetNextCar()
    {
        NextCar();
    }
}