using System;
using UnityEngine;
using UnityEngine.UI;

public class PodiumInputManager : MonoBehaviour
{
    private const string Game = "Game";
    private const string MainMenu = "Menu";
    
    public static event Action<string> PickedScene = delegate {  }; 
    public static event Action NextCar = delegate { };
    public static event Action PreviousCar = delegate {  };

    [Header("Button")]
    [SerializeField]
    private Button _game = null;
    [SerializeField]
    private Button _mainMenu = null;
    [SerializeField]
    private Button _previous = null;
    [SerializeField]
    private Button _next = null;

    private void Start()
    {
        _game.onClick.AddListener(StartGame);
        _mainMenu.onClick.AddListener(Menu);
        _previous.onClick.AddListener(Previous);
        _next.onClick.AddListener(Next);
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