using UnityEngine;

namespace Project.Dev.Scripts
{
    public abstract class Car : MonoBehaviour
    {
        protected float _speed = 0;
        protected float _speedTurn = 0;
        protected float _boost = 0;
        protected float _brake = 0;
    }
}