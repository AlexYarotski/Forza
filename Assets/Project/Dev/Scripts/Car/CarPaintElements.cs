using UnityEngine;

namespace Project.Dev.Scripts
{
    public class CarPaintElements : MonoBehaviour
    {
        [SerializeField]
        private Renderer[] paintElements = null;

        public Renderer[] PaintElements => paintElements;
    }
}