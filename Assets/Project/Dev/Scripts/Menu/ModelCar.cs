using UnityEngine;

public class ModelCar : MonoBehaviour
{
    [SerializeField]
    private float _startSpeedRotation = 0;
    [SerializeField]
    private float _speedRotation = 0;

    private bool _endSwiped = false;
    
    private Quaternion _nextRotation = Quaternion.identity;
    private Quaternion _dragRotation = Quaternion.identity;

    private void Awake()
    {
        _endSwiped = true;
        _dragRotation = transform.rotation;
    }

    private void OnEnable()
    {
        SwipeController.Dragged += SwipeController_Dragged;
        SwipeController.EndDragged += SwipeController_EndDragged;
    }

    private void OnDisable()
    {
        SwipeController.Dragged -= SwipeController_Dragged;
        SwipeController.EndDragged -= SwipeController_EndDragged;
    }

    private void Update()
    {
        if (_endSwiped)
        {
            transform.Rotate(0, _startSpeedRotation * Time.deltaTime, 0);
        }
        else
        {
            transform.rotation = _nextRotation;
            _dragRotation = transform.rotation;
        }
    }

    private void SwipeController_Dragged(Vector3 dragPositionVector3)
    {
        var newDragPosition = new Vector3(0, -dragPositionVector3.x, 0);

        _nextRotation.eulerAngles = _dragRotation.eulerAngles + newDragPosition * (_speedRotation * Time.deltaTime);
        _endSwiped = false;
    }

    private void SwipeController_EndDragged(Vector3 dragPositionVector3)
    {
        _endSwiped = true;
    }
}
