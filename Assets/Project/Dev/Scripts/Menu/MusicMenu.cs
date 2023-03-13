using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts.Menu
{
    public class MusicMenu : MonoBehaviour
    {
        private readonly string MusicProcent = "{0}%";
        
        [SerializeField]
        private AudioSource _audioSource = null;

        [SerializeField]
        private TextMeshProUGUI _musicTMP = null;

        [SerializeField]
        private Slider _musicSlider = null;

        [SerializeField]
        private TextMeshProUGUI _musicProcent = null;

        [SerializeField]
        private float _startValue = 0;

        private void Awake()
        {
            _musicSlider.value = _startValue;
        }

        private void Update()
        {
            _audioSource.volume = _musicSlider.value;
            _musicProcent.text = string.Format(MusicProcent, (int)(_audioSource.volume * 100));
        }
    }
}