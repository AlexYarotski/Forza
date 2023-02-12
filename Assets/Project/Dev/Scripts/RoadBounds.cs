﻿using UnityEngine;

namespace Project.Dev.Scripts
{
    public class RoadBounds : MonoBehaviour
    {
        [SerializeField]
        private float _leftBoundAxisX = 0;

        [SerializeField]
        private float _rightBoundAxisX = 0;

        public float LeftBoundAxisX => _leftBoundAxisX;
        public float RightBoundAxisX => _rightBoundAxisX;

        private void Awake()
        {
            CheckingBoundaries();
        }

        public bool IsInBounds(Vector3 position)
        {
            var positionAxisX = position.x;

            return positionAxisX <= _rightBoundAxisX && positionAxisX >= _leftBoundAxisX;
        }

        private void CheckingBoundaries()
        {
            if (_leftBoundAxisX == _rightBoundAxisX)
            {
                Debug.LogError("The borders cannot be equal!");
            }

            if (_leftBoundAxisX > _rightBoundAxisX)
            {
                Debug.LogError("The left border cannot be greater than the right!");
            }
        }
    }
}