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
                if (_cars[i].GetType().ToString() == PlayerPrefs.GetString(KeyCar))
                {
                    _cars[i].gameObject.SetActive(true);
                }
                else
                {
                    _cars[i].gameObject.SetActive(false);
                }
            }
        }
        
    }
}