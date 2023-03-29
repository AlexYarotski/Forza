using System.Collections;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Immortal : MonoBehaviour
    {
        [SerializeField]
        protected float _timeOfImmortality = 0;

        public IEnumerator MakeImmortal(Renderer[] renderers)
        {
            var timeImmortality = new WaitForSeconds(_timeOfImmortality);
            var boxCollider =  GetComponent<BoxCollider>();
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