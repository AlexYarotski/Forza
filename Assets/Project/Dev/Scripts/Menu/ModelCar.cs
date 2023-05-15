using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class ModelCar : MonoBehaviour
    {
        private const string KeyCar = "Car";

        private CarModelType _carModelType = default;
        private List<CarViewDataSettings.CarViewData> _сarViewDataList = new List<CarViewDataSettings.CarViewData>();
        private CarViewDataSettings.CarViewData _currentCar = null;

        private void Awake()
        {
            _carModelType = (CarModelType)PlayerPrefs.GetInt(KeyCar);
            
            CreateCarView();
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

        private void CreateCarView()
        {
            for (int i = 0; i < Enum.GetValues(typeof(CarModelType)).Length; i++)
            {
                var newCarViewData = SceneContexts.Instance.CarViewDataSettings.GetCarViewData((CarModelType)i);
                
                Instantiate(newCarViewData.CarView, transform);
                
                _сarViewDataList.Add(newCarViewData); 
            }

            _currentCar = _сarViewDataList.FirstOrDefault(cd => cd.CarModelType == _carModelType);
        }
        
        private void SetNewCar(bool next)
        {
            _currentCar.CarView.OffActive();
            
            if (next)
            {
                if (_сarViewDataList.IndexOf(_currentCar) == _сarViewDataList.Count - 1)
                {
                    _currentCar = _сarViewDataList[0];
                }
                else
                {
                    _currentCar = _сarViewDataList[_сarViewDataList.IndexOf(_currentCar) + 1];
                }
            }
            else
            {
                if (_сarViewDataList.IndexOf(_currentCar) == 0)
                {
                    _currentCar = _сarViewDataList[_сarViewDataList.Count - 1];
                }
                else
                {
                    _currentCar = _сarViewDataList[_сarViewDataList.IndexOf(_currentCar) - 1];
                }
            }
            
            _currentCar.CarView.OnActive();
              
            PlayerPrefs.SetInt(KeyCar,(int)_currentCar.CarModelType);
        }
    }
}