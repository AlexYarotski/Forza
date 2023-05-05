using UnityEngine;

namespace Project.Dev.Scripts
{
    public class CameraController : MonoBehaviour
    {
        private const string KeyCar = "Car";
        
        [SerializeField]
        private Car[] _cars = null;
        
        [SerializeField]
        private float _distanceToStop = 0;
        [SerializeField]
        private float _speedSilding = 0;
        
        private Vector3 _finalPosition = Vector3.zero;
        private float deltaPosAxisZ = 0;
        private bool _isCarDied = false;

        private Car _car = null;
        
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
            SetCar();
            
            deltaPosAxisZ = transform.position.z - _car.transform.position.z;
        }

        private void Update()
        {
            var position = transform.position;
            
            transform.position = !_isCarDied ? new Vector3(position.x, position.y, deltaPosAxisZ + _car.transform.position.z)
                : Vector3.Lerp(transform.position, _finalPosition, _speedSilding * Time.deltaTime);
        }
        
        private void Car_Died(Vector3 position)
        {
            _isCarDied = true;
            
            _finalPosition = new Vector3(transform.position.x, transform.position.y,
                position.z + _distanceToStop);
        }

        private void SetCar()
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                if (_cars[i].GetType().ToString() == PlayerPrefs.GetString(KeyCar))
                {
                    _cars[i].gameObject.SetActive(true);
                    _car = _cars[i];
                }

                _cars[i].gameObject.SetActive(false);
            }
            
            _cars[0].gameObject.SetActive(true);
            
            _car = _cars[0];
        }
    }
}