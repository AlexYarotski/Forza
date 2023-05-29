using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class Paint : MonoBehaviour
    {
        public static event Action<ColorName> ClickedButton = delegate {  };
        
        private const string KeyCar = "Car";

        private List<ColorButton> ButtonList = new List<ColorButton>();
        
        [SerializeField]
        private ColorButton _buttonPrefab = null;

        [SerializeField]
        private ColorButtonSetting _colorButtonSetting = null;

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
            
            InitColorButtons();
            EnableButtons(_modelCar);
        }
        
        private void CarViewPlaceholder_CarChanged(CarModelType carModelType)
        {
            EnableButtons(carModelType);
        }
        
        private void InitColorButtons()
        {
            foreach (ColorName colorName in Enum.GetValues(typeof(ColorName)))
            {
                var colorButton = Instantiate(_buttonPrefab, transform);
                
                colorButton.Setup(colorName, SetColor);
                colorButton.image.color = _colorButtonSetting.GetButtonColor(colorName);
                colorButton.Disable();
                
                ButtonList.Add(colorButton);
            }
        }

        private void EnableButtons(CarModelType carModelType)
        {
            var colorConfigs = _carDataSettings.GetCarData(carModelType).ColorSetting.ColorConfigs;

            for (int i = 0; i < ButtonList.Count; i++)
            {
                if (colorConfigs.Any(сс => сс.ColorName == ButtonList[i].ColorName))
                {
                    ButtonList[i].Enable();
                }
                else
                {
                    ButtonList[i].Disable();
                }
            }

            _modelCar = carModelType;
        }

        private void SetColor(ColorName colorName)
        {
            ClickedButton(colorName);
        }
    }
}