﻿using System;
using UnityEngine;

public class CarView : MonoBehaviour
{
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
    
    public void EnableCarView(CarModelType carModelType)
    {
        for (var i = 0; i < Enum.GetValues(typeof(CarModelType)).Length; i++)
        {
            var currentType = (CarModelType)i;

            if (currentType == carModelType)
            {
                GetPaintElements(currentType).Enable();
            }
            else
            {
                GetPaintElements(currentType).Disable();
            }
        }
    }

    public void PaintElement(CarModelType carModelType, ColorName colorName)
    {
        var paintElements = GetPaintElements(carModelType).Elements;
        var carData = SceneContexts.Instance.CarDataSetting.GetCarData(carModelType);
        
        for (int i = 0; i < paintElements.Length; i++)
        {
            paintElements[i].sharedMaterial = carData.ColorSetting.SelectMaterial(colorName);
        }
    }
}