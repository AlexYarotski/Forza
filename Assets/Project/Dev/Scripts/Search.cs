using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Search
    {
        public static Car ActiveCar(Car[] cars)
        {
            for (int i = 0; i < cars.Length; i++)
            {
                if (cars[i].gameObject.activeSelf)
                {
                    return cars[i];
                }
            }

            Debug.LogError("No active machines!");
            
            return null;
        }
    }
}