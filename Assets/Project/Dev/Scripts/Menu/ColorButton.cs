using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorButton : Button
{
    private Action<ColorName> _callback = null;

    public ColorName ColorName { get; private set; }

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

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}