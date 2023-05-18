using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class UICarInfo : MonoBehaviour
    {
        private const string KeyCar = "Car";

        [Header("TMP")]
        [SerializeField]
        private TextMeshProUGUI _name = null;
        [SerializeField]
        private TextMeshProUGUI _startSpeed = null;
        [SerializeField]
        private TextMeshProUGUI _maxSpeed = null;
        [SerializeField]
        private TextMeshProUGUI _health = null;
    
        [Space(15)]
        [SerializeField]
        private float _speedDivider = 0;
    
        [Header("Slider")]
        [SerializeField]
        private Slider _sliderStartSpeed = null;
        [SerializeField]
        private Slider _sliderMaxSpeed = null;
        [SerializeField]
        private Slider _sliderHealth = null;

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
            
            ModelCar_ChangedCar(_modelCar);
        }

        private void SetValue(CarDataSettings.CarData car)
        {
            var startSpeed = (int)car.Speed + 50;
            var maxSpeed = (int)car.MaxSpeed + 50;
            var health = (float)car.Health / 10;
        
            _sliderStartSpeed.value = startSpeed / _speedDivider;
            _sliderMaxSpeed.value = maxSpeed / _speedDivider;
            _sliderHealth.value = health;
        
            _name.text = _modelCar.ToString();
            _startSpeed.text = Convert.ToString(startSpeed);
            _maxSpeed.text = Convert.ToString(maxSpeed);
            _health.text = Convert.ToString(car.Health);
        }
    
        private void ModelCar_ChangedCar(CarModelType carModelType)
        {
            var carData = _carDataSettings.GetCarData(carModelType);
            _modelCar = carData.CarModelType;
            
            SetValue(carData);
        }
    }
}