using System;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Score : MonoBehaviour
    {
        public static event Action<float> Boost = delegate {};
        
        [SerializeField]
        private float _speedBoostScore = 0;
        
        private float _score = 0;

        private void OnEnable()
        {
            Urus.Drove += Urus_Drove;
        }

        private void OnDisable()
        {
            Urus.Drove -= Urus_Drove;
        }

        private void FixedUpdate()
        {
            if (CanIncreaseSpeed())
            {
                SpeedUp();
            }
        }
        
        private void Urus_Drove(float drove)
        {
            _score = drove;
        }

        private bool CanIncreaseSpeed()
        {
            return _speedBoostScore <= _score;
        }

        private void SpeedUp()
        {
            _speedBoostScore *= 2;
            Boost(_speedBoostScore);
        }
    }
}