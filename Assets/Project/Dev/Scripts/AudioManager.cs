using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
   [SerializeField]
   private AudioSource _audioSource = null;

   public void SetClip(AudioName audioName, bool isPlay = false)
   {
      var audioClips = SceneContexts.Instance.AudioManagerSetting.GetAudioClips(audioName);

      _audioSource.clip = audioClips;

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
