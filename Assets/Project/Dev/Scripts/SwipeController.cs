using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IDragHandler
{
    public static event Action<Vector2> Dragged = delegate{  };

    private Vector2 _dragPosition = Vector2.zero;

    public void OnDrag(PointerEventData eventData)
    {
        _dragPosition = eventData.delta;

        Dragged(_dragPosition);
    }
}