using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class Paint : MonoBehaviour
    {
        private const string KeyAudiColor = "AudiColor";
        private const string KeyLadaColor = "LadaColor";
        
        private readonly Dictionary<Car, List<Button>> ButtonDictionary = new Dictionary<Car, List<Button>>();

        [SerializeField]
        private ColorButton _buttonPrefab = null;

        [SerializeField]
        private Renderer[] _audiElements = null;
        [SerializeField]
        private Renderer[] _ladaElements = null;
        
        [SerializeField]
        private Car[] _cars = null;

        private ColorSetting _colorSetting = null;
        private Car _activeCar = null;
        

        private void OnEnable()
        {
            //ModelCar.PickedCar += Podium_PickedCar;
        }
        
        private void OnDisable()
        {
            //ModelCar.PickedCar -= Podium_PickedCar;
        }

        private void Start()
        {
            SetDictionary();
            
            _activeCar = _cars[0];
            _colorSetting = _activeCar.ColorSetting;
            
            SetActiveButton(true);
        }
        
        private void Podium_PickedCar(Car car)
        {
            SetActiveButton(false);
            
            _activeCar = car;
            _colorSetting = _activeCar.ColorSetting;
            
            SetActiveButton(true);
        }
        
        private void SetDictionary()
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                ButtonDictionary.Add(_cars[i], AddButton(_cars[i]));
            }
        }

        private void SetActiveButton(bool active)
        {
            var buttonList = ButtonDictionary[_activeCar];
            
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].gameObject.SetActive(active);
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
                //colorButton.Setup(colorConfigs[i].Colors, SetColor);
                colorButton.gameObject.SetActive(false);
                
                listButton.Add(colorButton);
            }

            return listButton;
        }

        // private void SetColor(Colors colors)
        // {
        //     if (_activeCar.GetType() == typeof(Audi))
        //     {
        //         for (int j = 0; j < _audiElements.Length; j++)
        //         {
        //             _audiElements[j].sharedMaterial = _colorSetting.SelectMaterial(colors);
        //
        //             PlayerPrefs.SetInt(KeyAudiColor, (int)colors);
        //         }
        //     }
        //     else if (_activeCar.GetType() == typeof(Lada))
        //     {
        //         for (int j = 0; j < _ladaElements.Length; j++)
        //         {
        //             _ladaElements[j].sharedMaterial = _colorSetting.SelectMaterial(colors);
        //
        //             PlayerPrefs.SetInt(KeyLadaColor, (int)colors);
        //         }
        //     }
        // }
    }
}