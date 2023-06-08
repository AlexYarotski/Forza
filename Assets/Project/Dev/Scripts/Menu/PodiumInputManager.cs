using System;
using Project.Dev.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PodiumInputManager : MonoBehaviour
{
    private const string Game = "Game";
    private const string MainMenu = "Menu";
    
    public static event Action NextCar = delegate { };
    public static event Action PreviousCar = delegate {  };

    [FormerlySerializedAs("_game")]
    [Header("Button")]
    [SerializeField]
    private Button _gameButton = null;
    [FormerlySerializedAs("_mainMenu")]
    [SerializeField]
    private Button _mainMenuButton = null;
    [FormerlySerializedAs("_previous")]
    [SerializeField]
    private Button _previousButton = null;
    [FormerlySerializedAs("_next")]
    [SerializeField]
    private Button _nextButton = null;

    private void Start()
    {
        _gameButton.AddListener(StartGame);
        _mainMenuButton.AddListener(OpenMenu);
        _previousButton.AddListener(SetPreviousCar);
        _nextButton.AddListener(SetNextCar);
    }

    private void OpenMenu()
    {
        SceneLoader.Load(MainMenu);
    }
    
    private void StartGame()
    {
        SceneLoader.Load(Game);
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