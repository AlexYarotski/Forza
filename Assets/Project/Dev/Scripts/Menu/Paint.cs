using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class Paint : MonoBehaviour
    {
        private const string KeyCar = "Car";

        
        private readonly Dictionary<CarModelType, List<Button>> ButtonDictionary = new Dictionary<CarModelType, List<Button>>();

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
            SetButton(carModelType);
        }
        
        private void SetDictionary()
        {
            for (int i = 0; i < Enum.GetValues(typeof(CarModelType)).Length; i++)
            {
                var currentModelType = (CarModelType)i;
                
                ButtonDictionary.Add(currentModelType, AddButton(currentModelType));
            }
        }

        private List<Button> AddButton(CarModelType carModelType)
        {
            var colorConfigs = _carDataSettings.GetCarData(carModelType).ColorSetting.ColorConfigs;
            var listButton = new List<Button>();
            
            for (var i = 0; i < colorConfigs.Length; i++)
            {
                var colorButton = Instantiate(_buttonPrefab, transform);
                
                colorButton.image.color = colorConfigs[i].Color;
                colorButton.Setup(colorConfigs[i].ColorName, SetColor);
                colorButton.gameObject.SetActive(carModelType == _modelCar);
                
                listButton.Add(colorButton);
            }

            return listButton;
        }
        
        private void SetButton(CarModelType carModelType)
        {
            foreach (var pair in ButtonDictionary)
            {
                for (int j = 0; j < pair.Value.Count; j++)
                {
                    pair.Value[j].gameObject.SetActive(pair.Key == carModelType);
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