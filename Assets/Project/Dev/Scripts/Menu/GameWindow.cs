using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWindow : Window
{
    [SerializeField]
    private TextMeshProUGUI _scoreTMPUGUI = null;
    [SerializeField]
    private Color _color = Color.white;

    [SerializeField]
    private float _sizeOfIncreaseScore = 0;
    [SerializeField]
    private float _timeDelayScore = 0;

    [SerializeField]
    private Button _settingButton = null;

    public override bool IsPopUp
    {
        get => false;
    }

    private void OnEnable()
    {
        Car.Drove += Car_Drove;
        Score.Boost += Score_Boost;
        Car.Died += Car_Died;
    }

    private void OnDisable()
    {
        Car.Drove -= Car_Drove;
        Score.Boost -= Score_Boost;
        Car.Died -= Car_Died;
    }
    
    private void Start()
    {
        _settingButton.onClick.AddListener(OpenSetting);
    }

    private void OpenSetting()
    {
        
    }

    private void Car_Drove(Vector3 drove)
    {
        _scoreTMPUGUI.text = $"{(int)drove.z}";
    }

    private void Score_Boost(float obj)
    {
        StartCoroutine(StyleScore());
    }

    private void Car_Died(Vector3 obj)
    {
        gameObject.SetActive(false);
    }

    private void Restart()
    {
        Time.timeScale = 1;
        
        SceneLoader.Instance.LoadGame();
    }

    private void OpenGarage()
    {
        Time.timeScale = 1;
        
        SceneLoader.Instance.LoadGarage();
    }
    
    private IEnumerator StyleScore()
    {
        var delay = new WaitForSeconds(_timeDelayScore);

        _scoreTMPUGUI.color = _color;
        _scoreTMPUGUI.fontSize += _sizeOfIncreaseScore;

        yield return delay;

        _scoreTMPUGUI.color = Color.white;
        _scoreTMPUGUI.fontSize -= _sizeOfIncreaseScore;
    }
}
