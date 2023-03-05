using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour
    {
        [SerializeField]
        protected float _speed = 0;
        
        [SerializeField]
        protected float _speedTurn = 0;

        [SerializeField]
        protected float _boost = 0;

        [SerializeField]
        protected float _brake = 0;
    }
}