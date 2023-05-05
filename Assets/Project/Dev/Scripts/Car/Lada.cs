namespace Project.Dev.Scripts
{
    public class Lada : Car
    {
        private const string KeyColor = "LadaColor";

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