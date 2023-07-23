using System.Collections.Generic;
using UnityEngine;

public class AudioSwitcher : MonoBehaviour
{
    private readonly List<AudioSource> AudioSourceList = new List<AudioSource>();

    private AudioSource _currentAudioSource = null;

    private void Awake()
    {
        var windowArray = SceneContexts.Instance.AudioManagerSetting.GetAudioSources();

        for (var i = 0; i < windowArray.Length; i++)
        {
            windowArray[i].transform.SetParent(null);

            var newWindow = Instantiate(windowArray[i], transform);
            
            newWindow.gameObject.SetActive(false);
            
            AudioSourceList.Add(newWindow);
        }
    }
}
