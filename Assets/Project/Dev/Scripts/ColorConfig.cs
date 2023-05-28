using UnityEngine;
using System;
using Project.Dev.Scripts;

[Serializable]
public class ColorConfig
{
    [SerializeField]
    private ColorName _colorName = default;
    
    [SerializeField]
    private Material _material = null;

    public Material Material => _material;
    public ColorName ColorName => _colorName;
}