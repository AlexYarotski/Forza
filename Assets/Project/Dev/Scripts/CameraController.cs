using UnityEngine;

namespace Project.Dev.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Car _car = null;

        [SerializeField]
        private float _distanceToStop = 0;

        [SerializeField]
        private float _timeSilding = 0;
        
        private float deltaPosAxisZ = 0;

        private bool _isCarDied = false;
        
        private void OnEnable()
        {
            Car.Died += Car_Died;
        }

        private void OnDisable()
        {
            Car.Died -= Car_Died;
        }

        private void Start()
        {
            deltaPosAxisZ = transform.position.z - _car.transform.position.z;
        }

        private void Update()
        {
            if (!_isCarDied)
            {
                transform.position = new Vector3(0, transform.position.y, deltaPosAxisZ + _car.transform.position.z);
            }
        }
        
        private void Car_Died(Vector3 position)
        {
            _isCarDied = true;
            
            var finalPosition = new Vector3(transform.position.x, transform.position.y,
                deltaPosAxisZ + _car.transform.position.z + _distanceToStop);

            transform.position = Vector3.Lerp(transform.position, finalPosition, _timeSilding * Time.deltaTime);
        }
    }
}