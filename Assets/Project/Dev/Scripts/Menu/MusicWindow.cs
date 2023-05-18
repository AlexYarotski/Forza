using UnityEngine;

namespace Project.Dev.Scripts.Menu
{
    public class MusicWindow : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _music = null;

        private void OnEnable()
        {
            Car.Died += Car_Died;
        }

        private void OnDisable()
        {
            Car.Died -= Car_Died;
        }

        private void Car_Died(Vector3 obj)
        {
            _music.Stop();
        }
    }
}