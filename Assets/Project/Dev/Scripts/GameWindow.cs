using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class GameWindow : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreTMPUGUI = null;

        [SerializeField]
        private Color _color = Color.white;

        [SerializeField]
        private float _sizeOfIncreaseScore = 0;
        
        [SerializeField]
        private float _timeDelayScore = 0;

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
            Urus.Died += Urus_Died;
        }

        private void OnDisable()
        {
            Urus.Drove -= Urus_Drove;
            Score.Boost -= Score_Boost;
            Urus.Died -= Urus_Died;
        }
        
        private void Urus_Drove(Vector3 drove)
        {
            var droveZ = drove.z;
            _scoreTMPUGUI.text = Convert.ToString((int)droveZ);
        }
        
        private void Score_Boost(float obj)
        {
            StartCoroutine(StyleScore());
        }
        
        private void Urus_Died(float obj)
        {
            gameObject.SetActive(false);
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
}