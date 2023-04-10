using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Urus : Car
    {
        [SerializeField]
        protected float _timeOfImmortality = 0;

        protected void OnEnable() 
        {
            base.OnEnable();
        }
        
        protected void OnDisable()
        {
            base.OnDisable();
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

        private void PlaySmoke()
        {
            ParticleManager.Instance.Play(ParticleType.CarSmoke);
        }
    }
}