using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class CarInfo : MonoBehaviour
    {
        private const string KeyCar = "Car";
        
        [SerializeField]
        private Car _startCar = null;
        
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

        private void OnEnable()
        {
            //ModelCar.PickedCar += Podium_PickedCar;
        }

        private void OnDisable()
        {
           // ModelCar.PickedCar -= Podium_PickedCar;
        }

        private void Start()
        {
            Podium_PickedCar(_startCar);
        }

        private void SetValue(Car car)
        {
            var startSpeed = (int)car.Speed + 80;
            var maxSpeed = (int)car.MaxSpeed + 80;
            var health = (float)car.Health / 10;
        
            _sliderStartSpeed.value = startSpeed / _speedDivider;
            _sliderMaxSpeed.value = maxSpeed / _speedDivider;
            _sliderHealth.value = health;
        
            _name.text = car.name;
            _startSpeed.text = Convert.ToString(startSpeed);
            _maxSpeed.text = Convert.ToString(maxSpeed);
            _health.text = Convert.ToString(car.Health);
        }
    
        private void Podium_PickedCar(Car car)
        {
            SetValue(car);

            PlayerPrefs.SetString(KeyCar, $"{car.GetType()}");
        }
    }
}