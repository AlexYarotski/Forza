using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public static event Action<SwipeController> Turn = delegate{  };

    private float _pointClickAxisX = 0;
    private float _currentPositionAxisX = 0;

    public float PointClickAxisX => _pointClickAxisX;
    public float CurrentPositionAxisX => _currentPositionAxisX;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointClickAxisX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _currentPositionAxisX = eventData.position.x;
        Turn(this);
    }
}