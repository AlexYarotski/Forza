using UnityEngine;

namespace Project.Dev.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Car _car = null;

        private float deltaPosAxisZ = 0;

        private void Start()
        {
            deltaPosAxisZ = transform.position.z - _car.transform.position.z;
        }

        private void Update()
        {
            transform.position = new Vector3(0, transform.position.y, deltaPosAxisZ + _car.transform.position.z);
        }
    }
}