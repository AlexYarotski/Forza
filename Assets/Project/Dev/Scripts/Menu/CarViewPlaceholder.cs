using System;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class CarViewPlaceholder : MonoBehaviour
    {
        public static event Action<CarModelType> CarChanged = delegate {  };
        
        private const string KeyCar = "Car";

        [SerializeField]
        private CarView _carView = null;
        
        private CarModelType _modelType = default;
        private PaintElement _currentElement = null;

        private void Awake()
        {
            _modelType = (CarModelType)PlayerPrefs.GetInt(KeyCar);
            
            ActiveElement();
        }

        private void OnEnable()
        {
            PodiumInputManager.PreviousCar += CarSpecifications_PreviousCar;
            PodiumInputManager.NextCar += CarSpecifications_NextCar;
        }

        private void OnDisable()
        {
            PodiumInputManager.PreviousCar -= CarSpecifications_PreviousCar;
            PodiumInputManager.NextCar -= CarSpecifications_NextCar;
        }

        private void CarSpecifications_PreviousCar()
        {
            var newIndex = (int)_modelType - 1 < 0 ? Enum.GetValues(typeof(CarModelType)).Length - 1 : 
                (int)_modelType - 1;
            
            ChangeCar(newIndex);
        }
        
        private void CarSpecifications_NextCar()
        {
            var newIndex = (int)_modelType + 1 > Enum.GetValues(typeof(CarModelType)).Length - 1 ? 0 :
                (int)_modelType + 1;
            
            ChangeCar(newIndex);
        }

        private void ChangeCar(int newIndex)
        {
            var newElements = _carView.GetPaintElements((CarModelType)newIndex);
            _modelType = (CarModelType)newIndex;

            if (_currentElement != null)
            {
                _currentElement.Disable(); 
            }
            
            newElements.Enable();
        
            _currentElement = newElements;

            CarChanged(_modelType);
            
            PlayerPrefs.SetInt(KeyCar, (int)_modelType);
        }
        
        private void ActiveElement()
        {
            for (var i = 0; i < Enum.GetValues(typeof(CarModelType)).Length; i++)
            {
                var currentType = (CarModelType)i;
                
                if (i == PlayerPrefs.GetInt(KeyCar))
                {
                    _carView.GetPaintElements(currentType).Enable();
                    _currentElement = _carView.GetPaintElements(currentType);
                    
                    PlayerPrefs.SetInt(KeyCar, i);
                }
                else
                {
                    _carView.GetPaintElements(currentType).Disable();
                }
            }
        }
    }
}