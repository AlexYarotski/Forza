using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LossingWindow : Window
{
    private const string MainMenu = "Menu";
    private const string GarageScene = "Garage";
    private const string Game = "Game";

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
    private AudioSource _music = null;

    [SerializeField]
    private Image _frame = null;

    private WindowManager _windowManager = null;
    
    private void OnEnable()
    {
        Car.Died += Car_Died;
    }

    private void OnDisable()
    {
        Car.Died -= Car_Died;
    }
    
    private void Start()
    {
        _menu.AddListener(OpenMenu);
        _garage.AddListener(OpenGarage);
        _restart.AddListener(Restart);
        
        _windowManager = WindowManager.Instance;
        
        Hide();
    }
    
    private void Car_Died(Vector3 position)
    {
        Show();

        _score.text = string.Format(ScoreText, (int)position.z);
        
        _music.Stop();
    }

    private void OpenMenu()
    {
        UploadScene(MainMenu);
    }

    private void OpenGarage()
    {
        UploadScene(GarageScene);
    }
    
    private void Restart()
    {
        UploadScene(Game);
    }

    private void UploadScene(string scene)
    {
        DOTween.PauseAll();
        _windowManager.Load(scene);
    }
}
