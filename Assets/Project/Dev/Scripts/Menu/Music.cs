using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class Music : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _music = null;

        private void OnEnable()
        {
            Car.Died += Urus_Died;
        }

        private void OnDisable()
        {
            Car.Died -= Urus_Died;
        }

        private void Urus_Died(Vector3 obj)
        {
            _music.Stop();
        }
    }
}