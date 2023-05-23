using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class Paint : MonoBehaviour
    {
        private const string KeyCar = "Car";

        
        private readonly Dictionary<CarModelType, List<ColorButton>> ButtonDictionary = new Dictionary<CarModelType, List<ColorButton>>();

        [SerializeField]
        private CarView _carView = null;
        
        [SerializeField]
        private ColorButton _buttonPrefab = null;

        private CarModelType _modelCar = default;
        private CarDataSettings _carDataSettings = null;
        
        private void OnEnable()
        {
            CarViewPlaceholder.CarChanged += CarViewPlaceholder_CarChanged;
        }
        
        private void OnDisable()
        {
            CarViewPlaceholder.CarChanged -= CarViewPlaceholder_CarChanged;
        }

        private void Start()
        {
            _carDataSettings = SceneContexts.Instance.CarDataSettings;
            _modelCar = (CarModelType)PlayerPrefs.GetInt(KeyCar);
            
            SetDictionary();
        }
        
        private void CarViewPlaceholder_CarChanged(CarModelType carModelType)
        {
            EnableButtons(carModelType);
        }
        
        private void SetDictionary()
        {
            for (int i = 0; i < Enum.GetValues(typeof(CarModelType)).Length; i++)
            {
                var currentModelType = (CarModelType)i;
                
                ButtonDictionary.Add(currentModelType, InitColorButtons(currentModelType));
            }
        }

        private List<ColorButton> InitColorButtons(CarModelType carModelType)
        {
            var colorConfigs = _carDataSettings.GetCarData(carModelType).ColorSetting.ColorConfigs;
            var listButton = new List<ColorButton>();
            
            for (var i = 0; i < colorConfigs.Length; i++)
            {
                var colorButton = Instantiate(_buttonPrefab, transform);
                
                colorButton.image.color = colorConfigs[i].Color;
                colorButton.Setup(colorConfigs[i].ColorName, SetColor);
                colorButton.SetActive(carModelType == _modelCar);
                colorButton.Disable();
                
                listButton.Add(colorButton);
            }

            return listButton;
        }
        
        private void EnableButtons(CarModelType carModelType)
        {
            foreach (var pair in ButtonDictionary)
            {
                for (int j = 0; j < pair.Value.Count; j++)
                {
                    pair.Value[j].SetActive(pair.Key == carModelType);
                }
            }
            
            _modelCar = carModelType;
        }

        private void SetColor(ColorName colorName)
        {
            _carView.PaintElement(_modelCar, colorName);
            
            PlayerPrefs.SetInt(_modelCar.ToString(), (int)colorName);
        }
    }
}