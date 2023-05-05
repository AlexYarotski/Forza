namespace Project.Dev.Scripts
{
    public class Audi : Car
    {
        private const string KeyColor = "AudiColor";

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

        private void Update()
        {
            base.Update();
        }
    }
}