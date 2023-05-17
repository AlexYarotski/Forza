using UnityEngine;

namespace Project.Dev.Scripts.Interface
{
    public interface IColorable
    {
        Material SelectMaterial(ColorName colorName);
    }
}