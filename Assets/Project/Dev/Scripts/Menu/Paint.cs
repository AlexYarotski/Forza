using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class Paint : MonoBehaviour
    {
        private const string KeyUrusColor = "Uruscolor";
        private const string KeyLadaColor = "Ladacolor";
        
        private readonly Dictionary<Car, List<Button>> ButtonDictionary = new Dictionary<Car, List<Button>>();

        [SerializeField]
        private ColorButton _buttonPrefab = null;

        [SerializeField]
        private Renderer[] _urusElements = null;
        [SerializeField]
        private Renderer[] _ladaElements = null;
        
        [SerializeField]
        private Car[] _cars = null;

        private ColorSetting _colorSetting = null;
        private Car _activeCar = null;
        
        private void Awake()
        {
            SetDictionary();
            
            _activeCar = Search.ActiveCar(_cars);
            _colorSetting = _activeCar.ColorSetting;
            
            SetActiveButton(ButtonDictionary[_activeCar], true);
        }

        private void FixedUpdate()
        {
            if (_activeCar != Search.ActiveCar(_cars))
            {
                SetActiveButton(ButtonDictionary[_activeCar], false);
                
                _activeCar = Search.ActiveCar(_cars);
                _colorSetting = _activeCar.ColorSetting;

                SetActiveButton(ButtonDictionary[_activeCar], true);
            }
        }

        private void SetDictionary()
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                ButtonDictionary.Add(_cars[i], AddButton(_cars[i]));
            }
        }

        private void SetActiveButton(List<Button> buttons, bool active)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(active);
            }
        }

        private List<Button> AddButton(Car car)
        {
            var colorConfigs = car.ColorSetting.ColorConfigs;
            var listButton = new List<Button>();
            
            for (var i = 0; i < colorConfigs.Length; i++)
            {
                var colorButton = Instantiate(_buttonPrefab, transform);
                
                colorButton.image.color = colorConfigs[i].Color;
                colorButton.Setup(colorConfigs[i].Colors, SetColor);
                colorButton.gameObject.SetActive(false);
                
                listButton.Add(colorButton);
            }

            return listButton;
        }
        
        private void SetColor(Colors colors)
        {
            for (int i = 0; i < _urusElements.Length; i++)
            {
                if ((int)colors < 5)
                {
                    _urusElements[i].sharedMaterial = _colorSetting.SelectMaterial(colors);
                    PlayerPrefs.SetInt(KeyUrusColor, (int)colors);
                }
                else if ((int)colors >= 5)
                {
                    _ladaElements[i].sharedMaterial = _colorSetting.SelectMaterial(colors);
                    PlayerPrefs.SetInt(KeyLadaColor, (int)colors);
                }
            }
            
            
        }
    }
}