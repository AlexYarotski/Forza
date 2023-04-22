using UnityEngine;

namespace Project.Dev.Scripts
{
    public class CarPreparation : MonoBehaviour
    {
        private const string KeyCar = "Car";
        
        [SerializeField]
        private Car[] _cars = null;

        private void Awake()
        {
            ChooseCar();
        }

        private  void ChooseCar()
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                _cars[i].gameObject.SetActive(_cars[i].GetType().ToString() == PlayerPrefs.GetString(KeyCar));
            }
        }
    }
}