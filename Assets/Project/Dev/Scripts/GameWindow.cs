using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class GameWindow : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreTextMeshProUGUI = null;
        
        [SerializeField]
        private Color _color = Color.white;
        
        [SerializeField]
        private float _sizeOfIncreaseScore = 0;
        
        [SerializeField]
        private int _timeDelayScore = 0;

        private void Awake()
        {
            // var setting = SceneContexts.SceneContexts.Instance.GameWindowSetting;
            //
            // _color = setting.Color;
            // _sizeOfIncreaseScore = setting.SizeOfIncreaseScore;
            // _timeDelayScore = setting.TimeDelayScore;
        }

        private void OnEnable()
        {
            Urus.Drove += Urus_Drove;
            Score.Boost += Score_Boost;
        }

        private void OnDisable()
        {
            Urus.Drove -= Urus_Drove;
            Score.Boost -= Score_Boost;
        }
        
        private void Urus_Drove(float drove)
        {
            _scoreTextMeshProUGUI.text = Convert.ToString((int)drove);
        }
        
        private void Score_Boost(float obj)
        {
            StartCoroutine(StyleScore());
        }

        private IEnumerator StyleScore()
        {
            var delay = new WaitForSeconds(_timeDelayScore);
            
            _scoreTextMeshProUGUI.color = _color;
            _scoreTextMeshProUGUI.fontSize += _sizeOfIncreaseScore;
            
            yield return delay;
            
            _scoreTextMeshProUGUI.color = Color.white;
            _scoreTextMeshProUGUI.fontSize -= _sizeOfIncreaseScore;
        }
    }
}