using System;
using Project.Dev.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LossingWindow : MonoBehaviour
{
    private const string MainMenu = "Menu";
    private const string GarageScene = "Garage";
    private const string Game = "Game";
    
    public static event Action<string> PickedScene = delegate {  }; 

    private readonly string ScoreText = "Your Score \r\n {0}";
    
    [SerializeField]
    private TextMeshProUGUI _score = null;

    [SerializeField]
    private Button _menu = null;
    
    [SerializeField]
    private Button _garage = null;
    
    [SerializeField]
    private Button _restart = null;

    [SerializeField]
    private Image _frame = null;
    
    private void Awake()
    {
        _menu.onClick.AddListener(Menu);
        _garage.onClick.AddListener(Garage);
        _restart.onClick.AddListener(Restart);
        
        SetComponentsActive(false);
    }

    private void OnEnable()
    {
        Car.Died += Car_Died;
    }

    private void OnDisable()
    {
        Car.Died -= Car_Died;
    }
    
    private void Car_Died(Vector3 position)
    {
        SetComponentsActive(true);
        
        _score.text = string.Format(ScoreText, (int)position.z);
    }

    private void Menu()
    {
        PickedScene(MainMenu);
    }

    private void Garage()
    {
        PickedScene(GarageScene);
    }
    
    private void Restart()
    {
        PickedScene(Game);
    }

    private void SetComponentsActive(bool isActive)
    {
        _score.gameObject.SetActive(isActive);
        _menu.gameObject.SetActive(isActive);
        _garage.gameObject.SetActive(isActive);
        _restart.gameObject.SetActive(isActive);
        _frame.gameObject.SetActive(isActive);
    }
}
