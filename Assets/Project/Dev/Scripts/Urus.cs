using System;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Urus : Car
    {
        public static event Action<Vector3> Drove = delegate { };

        [SerializeField]
        protected float _timeOfImmortality = 0;

        private new void OnEnable()
        {
            base.OnEnable();
            
            Score.Boost += Score_Boost;
        }
        
        private new void OnDisable()
        {
            base.OnDisable();
            
            Score.Boost -= Score_Boost;
        }

        private void Update()
        {
            if (_health > 0)
            {
                MoveForward();
            
                SetTurn();

                Drove(transform.position);
            }
        }
        
        public override void GetDamage()
        {
            base.GetDamage();
        
            if (_health <= 0)
            {
                return;   
            }

            PlaySmoke();
            Brake();
            
            StartCoroutine(Immortal.MakeImmortal(this, _timeOfImmortality));
        }

        private void Score_Boost(float boost)
        {
            if (_speed <= _maxSpeed)
            {
                _startSpeed += _boost;
            }
        }

        private void PlaySmoke()
        {
            ParticleManager.Instance.Play(ParticleType.CarSmoke);
        }
    }
}