using System;
using Project.Dev.Scripts;
using UnityEngine;

[Serializable]
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
        public CarPaintElements CarPaintElements
        {
            get;
            private set;
        }
    }
    
    [field: SerializeField]
    private CarViewConfigs[] _carViewConfigs = null;

    private void Start()
    {
        for (var i = 0; i < _carViewConfigs.Length; i++)
        {
            _carViewConfigs[i].CarPaintElements.gameObject.SetActive((int)_carViewConfigs[i].CarModelType == PlayerPrefs.GetInt(KeyCar));
        }
    }
}