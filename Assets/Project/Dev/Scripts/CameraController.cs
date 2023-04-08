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
        //NOTE: INCORRECT NAMING 
        private float _speedSilding = 0;
        
        private Vector3 _finalPosition = Vector3.zero;
        
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
            transform.position = !_isCarDied ? new Vector3(transform.position.x, transform.position.y, deltaPosAxisZ + _car.transform.position.z)
                : Vector3.Lerp(transform.position, _finalPosition, _speedSilding * Time.deltaTime);
        }
        
        private void Car_Died(Vector3 position)
        {
            _isCarDied = true;
            
            _finalPosition = new Vector3(transform.position.x, transform.position.y,
                position.z + _distanceToStop);
        }
    }
}