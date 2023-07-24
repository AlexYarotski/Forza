using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerSetting", menuName = "Settings/AudioManagerSetting", order = 0)]
public class AudioManagerSetting : ScriptableObject
{
    [SerializeField]
    private AudioClip[] _audioClips = null;
}
