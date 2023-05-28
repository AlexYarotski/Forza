using System;
using UnityEngine;

namespace Project.Dev.Scripts
{
    [Serializable]
    public class ColorButton 
    {
        [SerializeField]
        private ColorName colorName = default;

        [SerializeField]
        private Color _color = default;
        
        public ColorName ColorName => colorName;
        public Color Color => _color;
    }
}