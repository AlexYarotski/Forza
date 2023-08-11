using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioManagerSetting", menuName = "Settings/AudioManagerSetting", order = 0)]
public class AudioManagerSetting : ScriptableObject
{
    [Serializable]
    public class AudioConfig
    {
        [field: SerializeField]
        public AudioName AudioName
        {
            get;
            private set;
        }

        [field: SerializeField]
        public AudioClip AudioClips
        {
            get;
            private set;
        }
    }

    [SerializeField]
    private AudioConfig[] _audioConfigs = null;
    
    public AudioClip GetAudioClips(AudioName audioName)
    {
        for (int i = 0; i < _audioConfigs.Length; i++)
        {
            if (_audioConfigs[i].AudioName == audioName)
            {
                return _audioConfigs[i].AudioClips;
            }
        }

#if UNITY_EDITOR
        Debug.LogError($"AudioClip type not found {audioName}"); 
#endif

        return null;
    }
}
