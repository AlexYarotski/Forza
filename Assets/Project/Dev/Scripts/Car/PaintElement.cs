using UnityEngine;

namespace Project.Dev.Scripts
{
    public class PaintElement : MonoBehaviour
    {
        [SerializeField]
        private Renderer[] _elements = null;

        public Renderer[] Elements => _elements;
        
        public void Enable()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].gameObject.SetActive(true);
            }
        }

        public void Disable()
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                _elements[i].gameObject.SetActive(false);
            }
        }
    }
}