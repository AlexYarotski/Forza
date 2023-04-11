using UnityEngine;
using System;
using Project.Dev.Scripts;

[Serializable]
public class ColorConfig
{
    [SerializeField]
    private Colors _colors = default;

    [SerializeField]
    private Color _color = default;

    [SerializeField]
    private Material _material = null;

    public Colors Colors => _colors;
    public Material Material=> _material;
    public Color Color => _color;
}