using System;
using System.Collections;
using Project.Dev.Scripts.Interface;
using Project.Dev.Scripts.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts
{
    public class GameWindow : MonoBehaviour, IEnableButtons
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
            // var setting = SceneContexts.SceneContexts.Instance.GameWindowSetting;
            //
            // _color = setting.Color;
            // _sizeOfIncreaseScore = setting.SizeOfIncreaseScore;
            // _timeDelayScore = setting.TimeDelayScore;
            
            _settingButton.onClick.AddListener(Setting);
            _setting.SetChildrenActiveState(false);
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

        private void FixedUpdate()
        {
            if (!_setting.IsActive)
            {
                EnableButtons(true);
            }
        }

        public void EnableButtons(bool enable)
        {
            _settingButton.gameObject.SetActive(enable);
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

        private void Setting()
        {
            Time.timeScale = 0;

            EnableButtons(false);
            _setting.SetChildrenActiveState(true);
        }
    }
}