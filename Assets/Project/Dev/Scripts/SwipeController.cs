using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IDragHandler
{
    public static event Action<Vector2> Dragged = delegate{  };

    public void OnDrag(PointerEventData eventData)
    {
        Dragged(eventData.delta);
    }
}