using System;
using UnityEngine;
using System.Linq;

namespace Project.Dev.Scripts
{
    [CreateAssetMenu(fileName = "CarDataSettings", menuName = "Settings/CarDataSettings", order = 0)]
    public class CarDataSettings : ScriptableObject
    {
        [Serializable]
        public class CarData
        {
            #region Property

            [field: SerializeField]
            public CarModelType CarModelType
            {
                get;
                private set;
            }
            
            [field: Header("Health")]
            [field: SerializeField]
            public int Health
            {
                get;
                private set;
            }
            [field: SerializeField]
            public float TimeOfImmortality
            {
                get;
                private set;
            }

            [field: Header("Speed")]
            [field: SerializeField]
            public float Speed 
            {
                get;
                private set;
            }
            [field: SerializeField]
            public float MaxSpeed 
            {
                get;
                private set;
            }
            [field: SerializeField]
            public float SpeedTurn 
            {
                get;
                private set;
            }
            [field: SerializeField]
            public float Brake 
            {
                get;
                private set;
            }
            [field: SerializeField]
            public float Boost 
            {
                get;
                private set;
            }

            [field: Header("Rotation")]
            [field: SerializeField]
            public float SpeedRotation 
            {
                get;
                private set;
            }
            [field: SerializeField]
            public float SpeedReturnRotation 
            {
                get;
                private set;
            }
            [field: SerializeField]
            public float RotationAngel 
            {
                get;
                private set;
            }

            [field: Header("Paint")]
            [field: SerializeField]
            public ColorSetting ColorSetting 
            {
                get;
                private set;
            }
            [field: SerializeField]
            public Renderer[] PainElements 
            {
                get;
                private set;
            }

            [field: Header("Other")]
            [field: SerializeField]
            public RoadBounds RoadBounds 
            {
                get;
                private set;
            }
            
            #endregion
        }
        
        [SerializeField]
        private CarData[] _carsData = null;

        public CarData GetCarData(CarModelType carModelType)
        {
            var carData = _carsData.FirstOrDefault(cd => cd.CarModelType == carModelType);

            if (carData == null)
            {
                Debug.LogError($"Car data with type {carModelType} is not found!");
                
                return null;
            }    

            return carData;
        }
    }
}
