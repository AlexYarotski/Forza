using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class ColorButton : Button
    {
        private Action<ColorName> _callback = null;

        public ColorName ColorName
        {
            get;
            internal set;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            
            _callback.Invoke(ColorName);
        }

        public void Setup(ColorName colorName, Action<ColorName> callback)
        {
            ColorName = colorName;
            _callback = callback;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}