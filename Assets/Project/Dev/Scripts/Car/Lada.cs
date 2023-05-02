using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Lada : Car
    {
        private const string KeyColor = "LadaColor";
        
        [SerializeField]
        protected float _timeOfImmortality = 0;

        protected void Awake()
        {
            SetColor(KeyColor);
            
            base.Awake();
        }
        
        protected void OnEnable() 
        {
            base.OnEnable();
        }
        
        protected void OnDisable()
        {
            base.OnDisable();
        }

        private void FixedUpdate()
        {
            base.FixedUpdate();
        }
        
        private void Update()
        {
            base.Update();
        }

        public override void GetDamage()
        {
            base.GetDamage();
        
            if (_health == 1)
            {
                PlaySmoke();    
            }    
            
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