using UnityEngine;

public class RotationModel : MonoBehaviour
{
    [SerializeField]
    private float _startSpeedRotation = 0;
    
    [SerializeField]
    private float _speedRotationSwipe = 0;

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
        if (_nextRotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _nextRotation, _speedRotationSwipe * Time.deltaTime);

            _dragRotation = transform.rotation;
            
            return;
        }
        
        Rotation();
    }

    private void SwipeController_Dragged(Vector3 dragPositionVector2)
    {
        var dragPositionVector3 = new Vector3(0, -dragPositionVector2.x, 0);

        _nextRotation.eulerAngles = _dragRotation.eulerAngles + dragPositionVector3;
    }

    private void Rotation()
    {
        var newRotation = Quaternion.identity;

        newRotation.eulerAngles = new Vector3(0, _startSpeedRotation + transform.rotation.eulerAngles.y, 0);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _startSpeedRotation * Time.deltaTime);
    }
}