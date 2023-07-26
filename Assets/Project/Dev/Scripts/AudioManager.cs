using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   private readonly List<AudioClip> AudioClipList = new List<AudioClip>();

   [SerializeField]
   private AudioSource _audioSource = null;

   private void Start()
   {
      var audioClips = SceneContexts.Instance.AudioManagerSetting.GetAudioClips();
      
      for (var i = 0; i < audioClips.Length; i++)
      {
         AudioClipList.Add(audioClips[i]);
      }
   }
   
   public void SetClip(string clipName, bool isPlay = false)
   {
      var audioClip = AudioClipList.FirstOrDefault(ac => ac.name == clipName);

      switch (clipName)
      {
         case "Main" : 
            _audioSource.clip = audioClip;
            break;
         
         case "Game" :
            _audioSource.clip = audioClip;
            break;
         
         default:
#if UNITY_EDITOR
            Debug.LogError($"Window type not found {clipName}");
#endif
            _audioSource.clip = null;
            break;
      }

      if ( _audioSource.clip != null && isPlay)
      {
         Play();
      }
   }

   public void SetVolume(float volume)
   {
      _audioSource.volume = volume;
   }
   
   public void Play()
   {
      _audioSource.Play();
   }

   public void Pause()
   {
      _audioSource.Pause();
   }

   public void Stop()
   {
      _audioSource.Stop();
   }
}
