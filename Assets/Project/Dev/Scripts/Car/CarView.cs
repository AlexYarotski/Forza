using System;
using Project.Dev.Scripts;
using UnityEngine;

public class CarView : MonoBehaviour
{
    private const string KeyCar = "Car";
    
    [Serializable]
    public class CarViewConfigs
    {
        [field: SerializeField]
        public CarModelType CarModelType
        {
            get;
            private set;
        }

        [field: SerializeField]
        public PaintElement PaintElement
        {
            get;
            private set;
        }
    }
    
    [SerializeField]
    private CarViewConfigs[] _carViewConfigs = null;

    private void Start()
    {
        ActiveCar();
    }

    public PaintElement GetPaintElements(CarModelType carModelType)
    {
        for (int i = 0; i < _carViewConfigs.Length; i++)
        {
            if (_carViewConfigs[i].CarModelType == carModelType)
            {
                return _carViewConfigs[i].PaintElement;
            }
        }
        
        Debug.LogError("The are no elements of this type!");
        
        return null;
    }

    private void ActiveCar()
    {
        for (var i = 0; i < _carViewConfigs.Length; i++)
        {
            if ((int)_carViewConfigs[i].CarModelType == PlayerPrefs.GetInt(KeyCar))
            {
                _carViewConfigs[i].PaintElement.Enable();
            }
            else
            {
                _carViewConfigs[i].PaintElement.Disable();
            }
        }
    }
}