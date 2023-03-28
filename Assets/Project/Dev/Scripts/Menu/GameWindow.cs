using System;
using System.Collections;
using Project.Dev.Scripts.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts
{
    public class GameWindow : UIWindow
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

        [SerializeField]
        private Setting _setting = null;

        private void Awake()
        {
            _settingButton.onClick.AddListener(Setting);
            
            _setting.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Urus.Drove += Urus_Drove;
            Score.Boost += Score_Boost;
            Car.Died += Urus_Died;
        }

        private void OnDisable()
        {
            Urus.Drove -= Urus_Drove;
            Score.Boost -= Score_Boost;
            Car.Died -= Urus_Died;
        }

        private void Urus_Drove(Vector3 drove)
        {
            _scoreTMPUGUI.text = Convert.ToString((int)drove.z);
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

        private void Setting()
        {
            Time.timeScale = 0;

            _setting.gameObject.SetActive(true);
        }
    }
}