using UnityEngine;
using System;
using Project.Dev.Scripts;
using UnityEngine.Serialization;

[Serializable]
public class ColorConfig
{
    [FormerlySerializedAs("_colors")]
    [SerializeField]
    private ColorName colorName = default;

    [SerializeField]
    private Color _color = default;

    [SerializeField]
    private Material _material = null;

    public ColorName ColorName => colorName;
    public Material Material=> _material;
    public Color Color => _color;
}