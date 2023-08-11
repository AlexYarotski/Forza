using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMusic : MonoBehaviour
{
    private readonly string MusicProcent = "{0}%";

    [SerializeField]
    private Slider _musicSlider = null;

    [SerializeField]
    private TextMeshProUGUI _musicProcent = null;

    [SerializeField]
    private float _startValue = 0;

    private AudioManager _audioManager = null;

    private void Start()
    {
        _audioManager = AudioManager.Instance;

        _musicSlider.value = _startValue;

        _audioManager.SetVolume(_musicSlider.value);
    }

    private void Update()
    {
        _audioManager.SetVolume(_musicSlider.value);

        _musicProcent.text = string.Format(MusicProcent, (int)(_musicSlider.value * 100));
    }
}