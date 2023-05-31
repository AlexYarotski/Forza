using System;
using DG.Tweening;
using Project.Dev.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PodiumInputManager : MonoBehaviour
{
    private const string Game = "Game";
    private const string MainMenu = "Menu";
    
    public static event Action<string> PickedScene = delegate {  }; 
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
        _mainMenuButton.AddListener(Menu);
        _previousButton.AddListener(Previous);
        _nextButton.AddListener(Next);
    }

    private void Menu()
    {
        PickedScene(MainMenu);
    }
    
    private void StartGame()
    {
        PickedScene(Game);
    }

    private void Previous()
    {
        PreviousCar();
    }

    private void Next()
    {
        NextCar();
    }
}