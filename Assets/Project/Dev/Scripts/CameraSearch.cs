using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraSearch : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;
    
    private void Awake()
    {
        var cameraData = _camera.GetUniversalAdditionalCameraData();
        var overlayCamera = UICamera.Instance;

        cameraData.cameraStack.Add(overlayCamera);
    }
}