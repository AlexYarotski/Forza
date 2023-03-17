using UnityEngine;

public class ModelCar : MonoBehaviour
{
    [SerializeField]
    private float _startSpeedRotation = 0;
    
    [SerializeField]
    private float _speedRotation = 0;

    private bool _hasSwiped = false;
    
    private Quaternion _nextRotation = Quaternion.identity;
    private Quaternion _dragRotation = Quaternion.identity;

    private void Awake()
    {
        _dragRotation = transform.rotation;
    }

    private void OnEnable()
    {
        SwipeController.Dragged += SwipeController_Dragged;
    }

    private void OnDisable()
    {
        SwipeController.Dragged -= SwipeController_Dragged;
    }

    private void Update()
    {
        if (_hasSwiped)
        {
            transform.rotation = _nextRotation;

            _dragRotation = transform.rotation;   
        }
        else
        {
            transform.Rotate(0, _startSpeedRotation * Time.deltaTime, 0);
        }
    }

    private void SwipeController_Dragged(Vector3 dragPositionVector2)
    {
        var dragPositionVector3 = new Vector3(0, -dragPositionVector2.x, 0);

        _nextRotation.eulerAngles = _dragRotation.eulerAngles + dragPositionVector3 * (_speedRotation * Time.deltaTime);

        _hasSwiped = true;
    }
}
