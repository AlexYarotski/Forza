using System;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class Podium : MonoBehaviour
    {
        public static event Action<Car> PickedCar = delegate{  };
        
        [SerializeField]
        private Car[] _cars = null;

        private Car _car = null;

        private void Awake()
        {
            _car = _cars[0];
        }

        private void OnEnable()
        {
            CarSpecifications.PreviousCar += CarSpecifications_PreviousCar;
            CarSpecifications.NextCar += CarSpecifications_NextCar;
        }

        private void OnDisable()
        {
            CarSpecifications.PreviousCar -= CarSpecifications_PreviousCar;
            CarSpecifications.NextCar -= CarSpecifications_NextCar;
        }

        private void CarSpecifications_PreviousCar()
        {
            _car.gameObject.SetActive(false);

            var index = Array.IndexOf(_cars, _car);
            
            _car = index == 0 ? _cars[_cars.Length - 1] : _cars[index - 1];
            
            _car.gameObject.SetActive(true);
            
            PickedCar(_car);
        }

        private void CarSpecifications_NextCar()
        {
            _car.gameObject.SetActive(false);

            var index = Array.IndexOf(_cars, _car);
            
            _car = index == _cars.Length - 1 ? _cars[0] : _cars[index +1];
            
            _car.gameObject.SetActive(true);

            PickedCar(_car);
        }
    }
}