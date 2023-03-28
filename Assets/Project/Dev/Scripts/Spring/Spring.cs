using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Spring : MonoBehaviour
    {
        [SerializeField]
        private float _angularFrequency = 0;

        [SerializeField]
        private float _dampingRatio = 0;

        private float _startDamping = 0;

        private void Awake()
        {
            _startDamping = _dampingRatio;
        }

        public void Position()
        {
            while (_dampingRatio >= 0)
            {
                var rightPosition = new Vector3(transform.position.x + _dampingRatio, 0.1f, transform.position.z);
                var leftPosition = new Vector3(transform.position.x - _dampingRatio, 0.1f, transform.position.z);

                transform.position = rightPosition;
                transform.position = leftPosition;

                _dampingRatio -= 0.1f;
            }

            _dampingRatio = _startDamping;

        }
    }
}