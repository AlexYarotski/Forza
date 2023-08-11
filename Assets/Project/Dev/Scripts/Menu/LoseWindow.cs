using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : Window
{
    private const float DurationMove = 1f;
    private const string KeyCurrentScore = "CurrentScore";
    
    private readonly Vector3 LosePosition = new Vector3(0, 400, 0);
    private readonly string ScoreText = "Your Score \r\n {0}";

    [SerializeField]
    private TextMeshProUGUI _scoreLabel = null;
    [SerializeField]
    private TextMeshProUGUI _loseLabel = null;

    [SerializeField]
    private Button _menu = null;
    [SerializeField]
    private Button _garage = null;
    [SerializeField]
    private Button _restart = null;

    [SerializeField]
    private Image _frameLose = null;

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
    
    public override void Show()
    {
        base.Show();

        AudioManager.Instance.Stop();
        
        _loseLabel.rectTransform.DOLocalMove(LosePosition, DurationMove);
                    
        _scoreLabel.text = string.Format(ScoreText, PlayerPrefs.GetInt(KeyCurrentScore));
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
