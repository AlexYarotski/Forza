using System;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        private float _speedTurn = 0;

        [SerializeField]
        private RoadBounds _roadBounds = null;

        private Vector3 _turn = Vector3.zero;

        private void Awake()
        {
            _turn = new Vector3(1, 0, 0) * _speedTurn;
        }

        private void OnEnable()
        {
            SwipeController.Turn += SwipeController_Turn;
        }

        private void OnDisable()
        {
            SwipeController.Turn -= SwipeController_Turn;
        }
        
        private void SwipeController_Turn(float firstPoint, float secondPoint)
        {
            Turn(firstPoint, secondPoint);
        }

        private void Turn(float firstPoint, float secondPoint)
        {
            if (_roadBounds.IsInBounds(transform))
            {
                if (IsTurnLeft(firstPoint, secondPoint))
                {
                    transform.position += _turn;
                }
                else
                {
                    transform.position -= _turn; 
                }
            }
        }

        private bool IsTurnLeft(float firstPoint, float secondPoint)
        {
            return secondPoint < firstPoint;
        }
    }
}