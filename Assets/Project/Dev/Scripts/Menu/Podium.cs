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
            SetNewCar(false);
        }

        private void CarSpecifications_NextCar()
        {
            SetNewCar(true);
        }

        private void SetNewCar(bool next)
        {
            _car.gameObject.SetActive(false);

            var index = Array.IndexOf(_cars, _car);
            
            if (next)
            {
                _car = index == _cars.Length - 1 ? _cars[0] : _cars[index +1];
            }
            else
            {
                _car = index == 0 ? _cars[_cars.Length - 1] : _cars[index - 1];
            }
            
            _car.gameObject.SetActive(true);

            PickedCar(_car);
        }
    }
}