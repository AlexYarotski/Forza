using System;
using Project.Dev.Scripts;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public static event Action<Vector3> Hit = delegate {  };
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Car car))
        {
            Hit(car.transform.position);
        }
    }
}
