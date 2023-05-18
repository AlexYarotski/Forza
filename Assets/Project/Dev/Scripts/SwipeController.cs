using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static event Action<Vector3> Dragged = delegate{  };
    public static event Action EndDragged = delegate{  };

    public void OnDrag(PointerEventData eventData)
    {
        Dragged(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragged();
    }
}