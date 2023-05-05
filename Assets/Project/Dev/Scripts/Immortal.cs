using System.Collections;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Immortal
    {
        private Car Car
        {
            get;
        }

        public Immortal(Car car)
        {
            Car = car;
        }
        
        public IEnumerator MakeImmortal(float timeOfImmortality)
        {
            var timeImmortality = new WaitForSeconds(timeOfImmortality);
            var boxCollider =  Car.GetComponent<BoxCollider>();
            var renderers = Car.GetComponentsInChildren<Renderer>();
            var startColor = new Color[renderers.Length];
            
            boxCollider.enabled = false;
            
            for (int i = 0; i < renderers.Length; i++)
            {
                startColor[i] = renderers[i].material.color;
                renderers[i].material.color = Color.magenta;
            }
        
            yield return timeImmortality;
        
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = startColor[i];
            }
            
            boxCollider.enabled = true;
        }
    }
}