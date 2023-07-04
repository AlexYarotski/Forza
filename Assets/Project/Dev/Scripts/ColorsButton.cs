using System;
using UnityEngine;

[Serializable]
public class ColorsButton
{
    [SerializeField]
    private ColorName colorName = default;

    [SerializeField]
    private Color _color = default;

    public ColorName ColorName => colorName;
    public Color Color => _color;
}
