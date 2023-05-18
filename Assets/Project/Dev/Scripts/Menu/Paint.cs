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
            CarViewPlaceholder.ChangedCar += ModelCar_ChangedCar;
        }
        
        private void OnDisable()
        {
            CarViewPlaceholder.ChangedCar -= ModelCar_ChangedCar;
        }

        private void Start()
        {
            _carDataSettings = SceneContexts.Instance.CarDataSettings;
            _modelCar = (CarModelType)PlayerPrefs.GetInt(KeyCar);
            
            SetDictionary();
            SetActiveButton(true);
        }
        
        private void ModelCar_ChangedCar(CarModelType modelType)
        {
            SetActiveButton(false);

            _modelCar = modelType;
            
            SetActiveButton(true);
        }
        
        private void SetDictionary()
        {
            for (int i = 0; i < Enum.GetValues(typeof(CarModelType)).Length; i++)
            {
                var currentModelType = (CarModelType)i;
                
                ButtonDictionary.Add(currentModelType, AddButton(currentModelType));
            }
        }

        private void SetActiveButton(bool active)
        {
            var buttonList = ButtonDictionary[_modelCar];
            
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].gameObject.SetActive(active);
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
                colorButton.gameObject.SetActive(false);
                
                listButton.Add(colorButton);
            }

            return listButton;
        }

        private void SetColor(ColorName colorName)
        {
            var paintElements = _carView.GetPaintElements(_modelCar).Elements;
            var carData = _carDataSettings.GetCarData(_modelCar);
            
            for (int i = 0; i < paintElements.Length; i++)
            {
                paintElements[i].sharedMaterial = carData.ColorSetting.SelectMaterial(colorName);
            }
            
            PlayerPrefs.SetInt(_modelCar.ToString(), (int)colorName);
        }
    }
}