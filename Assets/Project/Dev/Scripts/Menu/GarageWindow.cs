using System;
using UnityEngine;
using UnityEngine.UI;

public class GarageWindow : Window
{
    private const string KeyCar = "Car";

    public static event Action NextCar = delegate { };
    public static event Action PreviousCar = delegate {  };

    [SerializeField]
    private UILockCar _uiLockCar = null;
    [SerializeField]
    private Paint _paint = null;
    
    [Header("Button")]
    [SerializeField]
    private Button _gameButton = null;
    [SerializeField]
    private Button _mainMenuButton = null;
    [SerializeField]
    private Button _previousButton = null;
    [SerializeField]
    private Button _nextButton = null;

    private CarModelType _carModel = default;
    
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

    public override void Show()
    {
        base.Show();
        
         _carModel = (CarModelType)PlayerPrefs.GetInt(KeyCar);
        
        _uiLockCar.CarViewPlaceholder_CarChanged(_carModel);
        _paint.EnableButtons(_carModel);
    }

    private void OpenMenu()
    {
        _carModel = (CarModelType)PlayerPrefs.GetInt(KeyCar);
        var isCarUnlock = SceneContexts.Instance.LockCarSetting.IsCarUnlocked(_carModel);
        
        if (!isCarUnlock)
        {
            PlayerPrefs.SetInt(KeyCar, default);
        }

        SceneLoader.Instance.LoadMain();
    }
    
    private void StartGame()
    {
        if (CheckLockCar())
        {
            SceneLoader.Instance.LoadGame();
        }
    }

    private void SetPreviousCar()
    {
        PreviousCar();
    }

    private void SetNextCar()
    {
        NextCar();
    }

    private bool CheckLockCar()
    {
        _carModel = (CarModelType)PlayerPrefs.GetInt(KeyCar);
        var isCarUnlock = SceneContexts.Instance.LockCarSetting.IsCarUnlocked(_carModel);
        
        if (!isCarUnlock)
        {
            _uiLockCar.ActivateLock();

            return false;
        }

        return true;
    }
}