using UnityEngine;

namespace Project.Dev.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Car _car = null;

        private Vector3 deltaPos;

        private void Start()
        {
            deltaPos = transform.position - _car.transform.position;
        }

        private void Update()
        {
            transform.position = _car.transform.position + deltaPos;
        }
    }
}