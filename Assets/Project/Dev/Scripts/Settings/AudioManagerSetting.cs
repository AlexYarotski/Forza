using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerSetting", menuName = "Settings/AudioManagerSetting", order = 0)]
public class AudioManagerSetting : ScriptableObject
{
    [SerializeField]
    private AudioSource[] _audioSources = null;

    public AudioSource[] GetAudioSources()
    {
        return _audioSources;
    }
}