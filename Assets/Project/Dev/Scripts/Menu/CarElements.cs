using System;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class CarElements : MonoBehaviour
    {
        public static event Action<CarModelType> ChangedCar = delegate {  };
        
        private const string KeyCar = "Car";

        [SerializeField]
        private CarView _carView = null;
        
        private CarModelType _modelType = default;
        private PaintElement _currentElement = null;

        private void Awake()
        {
            _modelType = (CarModelType)PlayerPrefs.GetInt(KeyCar);
            _currentElement = _carView.GetPaintElements(_modelType);
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
            
            SetNewCarElements(newIndex);
        }
        
        private void CarSpecifications_NextCar()
        {
            var newIndex = (int)_modelType + 1 > Enum.GetValues(typeof(CarModelType)).Length - 1 ? 0 :
                (int)_modelType + 1;
            
            SetNewCarElements(newIndex);
        }

        private void SetNewCarElements(int newIndex)
        {
            var newElements = _carView.GetPaintElements((CarModelType)newIndex);
            _modelType = (CarModelType)newIndex;
            
            _currentElement.Disable();
            newElements.Enable();
        
            _currentElement = newElements;

            ChangedCar(_modelType);
        }
    }
}