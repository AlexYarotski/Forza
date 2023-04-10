using Project.Dev.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class Paint : MonoBehaviour, IColorable
    {
        [Header("Button")]
        [SerializeField]
        private Button _yellow = null;
        [SerializeField]
        private Button _green = null;
        [SerializeField]
        private Button _purple = null;
        [SerializeField]
        private Button _red = null;
        [SerializeField]
        private Button _white = null;

        [Space]
        [SerializeField]
        private ColorSetting _config = null;
        
        [Header("PaintElements")]
        [SerializeField]
        private Renderer[] _modelElements = null;
        
        private void Awake()
        {
            _yellow.onClick.AddListener(Yellow);
            _green.onClick.AddListener(Green);
            _purple.onClick.AddListener(Purple);
            _red.onClick.AddListener(Red);
            _white.onClick.AddListener(White);
        }

        public void SetColor(Colors color)
        {
            var colorConfigs = _config.ColorConfigs;
            
            for (int i = 0; i < colorConfigs.Length; i++)
            {
                if (colorConfigs[i].Colors == color)
                {
                    for (int j = 0; j < _modelElements.Length; j++)
                    {
                        _modelElements[j].sharedMaterial = colorConfigs[i].Material;
                    }
                }
            }
            
            PlayerPrefs.SetInt("color", (int)color);
        }
        
        private void Yellow()
        {
            SetColor(Colors.Yellow);
        }
        
        private void Green()
        {
            SetColor(Colors.Green);
        }
        
        private void Purple()
        {
            SetColor(Colors.Purple);
        }
        
        private void Red()
        {
            SetColor(Colors.Red);
        }
        
        private void White()
        {
            SetColor(Colors.White);
        }
    }
}