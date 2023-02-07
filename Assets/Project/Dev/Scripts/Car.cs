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
        private Vector3 _turningPosition = Vector3.zero;

        private void Awake()
        {
            _turn = Vector3.right * _speedTurn;
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
            if (_roadBounds.IsInBounds(transform.position))
            {
                TurnWithin(firstPoint, secondPoint);
            }
            else
            {
                TurnAtBorder(firstPoint, secondPoint);
            }
            
            transform.position = _turningPosition;
        }
            
        private bool IsTurnLeft(float firstPoint, float secondPoint)
        {
            return secondPoint < firstPoint;
        }

        private void TurnWithin(float firstPoint, float secondPoint)
        {
                _turningPosition = IsTurnLeft(firstPoint, secondPoint) ? transform.position += _turn :
                    transform.position -= _turn;
        }

        private void TurnAtBorder(float firstPoint, float secondPoint)
        {
            if (_roadBounds.LeftBoundAxisX == transform.position.x)
            {
                _turningPosition = IsTurnLeft(firstPoint, secondPoint) ? transform.position :
                    transform.position -= _turn;
            }
            else if (_roadBounds.RightBoundAxisX == transform.position.x)
            {
                _turningPosition = IsTurnLeft(firstPoint, secondPoint) ? transform.position += _turn :
                    transform.position;
            }
        }
    }
}