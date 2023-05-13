using System;
using System.Linq;
using UnityEngine;

namespace Project.Dev.Scripts
{
    [CreateAssetMenu(fileName = "CarViewDataSettings", menuName = "Settings/CarViewDataSettings", order = 0)]
    public class CarViewDataSettings : ScriptableObject
    {
        [Serializable]
        public class CarViewData 
        {
            [field: SerializeField]
            public CarModelType CarModelType
            {
                get;
                private set;
            }

            [field: SerializeField]
            public CarView CarView
            {
                get; 
                private set;
            } 
        }
        
        [SerializeField]
        private CarViewData[] _carsViewData = null;

        public CarViewData GetCarViewData(CarModelType carModelType)
        {
            var carViewData = _carsViewData.FirstOrDefault(cd => cd.CarModelType == carModelType);

            if (carViewData == null)
            {
                Debug.LogError($"Car data with type {carModelType} is not found!");
                
                return null;
            }    

            return carViewData;
        }
    }
}