using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWindow : Window
{
    private const string SpeedLabel = "{0}\r\nkm/h";
    
    [SerializeField]
    private TextMeshProUGUI _scoreLabel = null;
    [SerializeField]
    private Color _color = Color.white;
    [SerializeField]
    private TextMeshProUGUI _speedLabel = null;

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
    }

    private void OnDisable()
    {
        Car.Drove -= Car_Drove;
        Score.Boost -= Score_Boost;
    }
    
    private void Start()
    {
        _settingButton.onClick.AddListener(OpenSetting);
    }

    private void OpenSetting()
    {
        WindowSwitcher.Instance.Show<GameSettingWindow>();
    }

    private void Car_Drove(Vector3 drove, float speed)
    {
        _scoreLabel.text = $"{(int)drove.z}";

        _speedLabel.text = string.Format(SpeedLabel, Math.Round(speed, 1) + 50);
    }

    private void Score_Boost(float obj)
    {
        StartCoroutine(StyleScore());
    }
    
    private IEnumerator StyleScore()
    {
        var delay = new WaitForSeconds(_timeDelayScore);

        _scoreLabel.color = _color;
        _scoreLabel.fontSize += _sizeOfIncreaseScore;

        yield return delay;

        _scoreLabel.color = Color.white;
        _scoreLabel.fontSize -= _sizeOfIncreaseScore;
    }
}
