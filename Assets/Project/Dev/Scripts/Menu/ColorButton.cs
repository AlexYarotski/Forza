using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class ColorButton : Button
    {
        private Action<Colors> _callback = null;

        public Colors Color
        {
            get;
            internal set;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            
            _callback.Invoke(Color);
        }

        public void Setup(Colors color, Action<Colors> callback)
        {
            Color = color;
            _callback = callback;
        }
    }
}