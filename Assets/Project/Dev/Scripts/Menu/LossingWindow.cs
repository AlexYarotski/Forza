using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LossingWindow : Window
{
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

    public override bool IsPopUp
    {
        get => true;
    }

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
        SceneLoader.Instance.LoadMain();
    }

    private void OpenGarage()
    {
        SceneLoader.Instance.LoadGarage();
    }
    
    private void Restart()
    {
        SceneLoader.Instance.LoadGame();
    }
}
