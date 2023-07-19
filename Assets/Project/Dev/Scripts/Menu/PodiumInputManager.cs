using System;
using UnityEngine;
using UnityEngine.UI;

public class PodiumInputManager : MonoBehaviour
{
    private const string Game = "Game";
    private const string MainMenu = "Menu";
    private const string KeyCar = "Car";

    public static event Action NextCar = delegate { };
    public static event Action PreviousCar = delegate {  };

    [SerializeField]
    private UILockCar uiLockCar = null;
    
    [Header("Button")]
    [SerializeField]
    private Button _gameButton = null;
    [SerializeField]
    private Button _mainMenuButton = null;
    [SerializeField]
    private Button _previousButton = null;
    [SerializeField]
    private Button _nextButton = null;
    
    private WindowManager _windowManager = null;

    private void Start()
    {
        _gameButton.AddListener(StartGame);
        _mainMenuButton.AddListener(OpenMenu);
        _previousButton.AddListener(SetPreviousCar);
        _nextButton.AddListener(SetNextCar);

        _windowManager = WindowManager.Instance;
    }

    private void OpenMenu()
    {
        _windowManager.Load(MainMenu);
    }
    
    private void StartGame()
    {
        var carModel = (CarModelType)PlayerPrefs.GetInt(KeyCar);
        var isCarUnlock = SceneContexts.Instance.LockCarSetting.IsCarUnlocked(carModel);

        if (!isCarUnlock && carModel != default)
        {
            uiLockCar.ActivateLock();
            
            return;
        }
        
        _windowManager.Load(Game);
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