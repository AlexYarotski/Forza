using System;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class Paint : MonoBehaviour
    {
        private const string KeyColor = "color";
        
        [SerializeField]
        private ColorButton _buttonPrefab = null;

        [SerializeField]
        private ColorSetting _config = null;

        [SerializeField]
        private Renderer[] _modelElements = null;

        private void Awake()
        {
            AddButton();
        }

        private void AddButton()
        {
            var paintColors = (Colors[])Enum.GetValues(typeof(Colors));

            for (var i = 0; i < paintColors.Length; i++)
            {
                var colorButton = Instantiate(_buttonPrefab, transform);

                colorButton.image.color = _config.ColorConfigs[i].Color;
                colorButton.Setup(paintColors[i], SetColor);
            }
        }
        
        private void SetColor(Colors colors)
        {
            for (int i = 0; i < _modelElements.Length; i++)
            {
                _modelElements[i].sharedMaterial = _config.SelectMaterial(colors);
            }
            
            PlayerPrefs.SetInt(KeyColor, (int)colors);
        }
    }
}