using UnityEngine;
using UnityEngine.Audio;

namespace AudioContent
{
    public class AudioService : MonoBehaviour
    {
        public static AudioService Instance;

        private const string Music = "Music";
        private const string SFX = "SFX";
        private const float OnVolume = 0f;
        private const float OffVolume = -80f;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSource _audioOneShotPlayer;

        private bool musicEnabled = true;
        private bool sfxEnabled = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat(Music, volume);
        }

        public void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat(SFX, volume);
        }
        
        public void ToggleSFX()
        {
            sfxEnabled = !sfxEnabled;
            SetSFXVolume(sfxEnabled ? OnVolume : OffVolume);
        }
        
        public void ToggleMusic()
        {
            musicEnabled = !musicEnabled;
            SetMusicVolume(musicEnabled ? OnVolume : OffVolume);
        }

        public void PlayClip(AudioClip clip)
        {
            _audioOneShotPlayer.PlayOneShot(clip);
        }

        public bool IsMusicEnabled() => musicEnabled;

        public bool IsSFXEnabled() => sfxEnabled;
    }
}