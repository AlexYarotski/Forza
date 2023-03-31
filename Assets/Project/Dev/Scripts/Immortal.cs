using System.Collections;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Immortal 
    {
        public static IEnumerator MakeImmortal(Car car, float timeOfImmortality)
        {
            var timeImmortality = new WaitForSeconds(timeOfImmortality);
            var boxCollider =  car.GetComponent<BoxCollider>();
            var renderers = car.GetComponentsInChildren<Renderer>();
            var startColor = new Color[renderers.Length];
            
            boxCollider.enabled = false;
            
            for (int i = 0; i < renderers.Length; i++)
            {
                startColor[i] = renderers[i].material.color;
                renderers[i].material.color = Color.red;
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