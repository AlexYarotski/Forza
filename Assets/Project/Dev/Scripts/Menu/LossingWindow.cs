using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LossingWindow : MonoBehaviour
{
    private const string MainMenu = "Menu";
    private const string GarageScene = "Garage";
    private const string Game = "Game";
    
    public static event Action<string> PickedScene = delegate {  };

    private const float DurationScore = 1f;
    private const float DurationLose = 1f;
    
    private readonly Vector2 LosePosition = new Vector2(0, -600);
    private readonly string ScoreText = "Your Score \r\n {0}";

    [SerializeField]
    private TextMeshProUGUI _score = null;

    [SerializeField]
    private TextMeshProUGUI _lose = null;

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
        _menu.AddListener(Menu);
        _garage.AddListener(Garage);
        _restart.AddListener(Restart);
        
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

        _frame.rectTransform.DOAnchorPos(Vector3.zero, DurationScore).SetEase(Ease.Linear);
        _lose.rectTransform.DOAnchorPos(LosePosition, DurationLose).SetEase(Ease.Linear);
        
        _score.text = string.Format(ScoreText, (int)position.z);
    }

    private void Menu()
    {
        DOTween.PauseAll();
        PickedScene(MainMenu);
    }

    private void Garage()
    {
        DOTween.PauseAll();
        PickedScene(GarageScene);
    }
    
    private void Restart()
    {
        DOTween.PauseAll();
        PickedScene(Game);
    }

    private void SetComponentsActive(bool isActive)
    {
        _menu.gameObject.SetActive(isActive);
        _garage.gameObject.SetActive(isActive);
        _restart.gameObject.SetActive(isActive);
    }
}
