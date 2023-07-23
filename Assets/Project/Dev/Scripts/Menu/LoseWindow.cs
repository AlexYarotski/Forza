using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : Window
{
    private const float DurationMove = 1f;
    
    private readonly Vector3 LosePosition = new Vector3(0, 400, 0);
    private readonly string ScoreText = "Your Score \r\n {0}";

    [SerializeField]
    private TextMeshProUGUI _scoreTMP = null;
    [SerializeField]
    private TextMeshProUGUI _loseTMP = null;

    [SerializeField]
    private Button _menu = null;
    [SerializeField]
    private Button _garage = null;
    [SerializeField]
    private Button _restart = null;

    [SerializeField]
    private Image _frameLose = null;

    private int _score = 0;
    
    public override bool IsPopUp
    {
        get => true;
    }
    
    private void Start()
    {
        _menu.AddListener(OpenMenu);
        _garage.AddListener(OpenGarage);
        _restart.AddListener(Restart);
    }

    private void OnEnable()
    {
        Car.Died += Car_Died;
    }

    private void Car_Died(Vector3 position)
    {
        _score = (int)position.z;
    }

    public override void Show()
    {
        base.Show();

        _loseTMP.rectTransform.DOLocalMove(LosePosition, DurationMove);
                    
        _scoreTMP.text = string.Format(ScoreText, _score);
        _frameLose.rectTransform.DOLocalMove(Vector3.zero, DurationMove);
    }

    private void OpenMenu()
    {
        SceneLoader.Instance.LoadMain();
        
        Hide();
    }

    private void OpenGarage()
    {
        SceneLoader.Instance.LoadGarage();
        
        Hide();
    }
    
    private void Restart()
    {
        SceneLoader.Instance.LoadGame();
        
        Hide();
    }
}
