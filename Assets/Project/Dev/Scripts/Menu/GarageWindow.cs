using System;
using UnityEngine;
using UnityEngine.UI;

public class GarageWindow : Window
{
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

    public override bool IsPopUp
    {
        get => false;
    }

    private void Start()
    {
        _gameButton.AddListener(StartGame);
        _mainMenuButton.AddListener(OpenMenu);
        _previousButton.AddListener(SetPreviousCar);
        _nextButton.AddListener(SetNextCar);
    }

    private void OpenMenu()
    {
        SceneLoader.Instance.LoadMain();
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
        
        SceneLoader.Instance.LoadGame();
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