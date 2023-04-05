using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Urus : Car
    {
        [SerializeField]
        protected float _timeOfImmortality = 0;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            Score.Boost += Score_Boost;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            
            Score.Boost -= Score_Boost;
        }

        private void Update()
        {
            base.Update();
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

            var immortal = new Immortal(this);
            StartCoroutine(immortal.MakeImmortal(_timeOfImmortality));
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