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

        private bool _musicEnabled = true;
        private bool _sfxEnabled = true;

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

        public void ToggleSFX()
        {
            _sfxEnabled = !_sfxEnabled;
            SetSFXVolume(_sfxEnabled ? OnVolume : OffVolume);
        }

        public void ToggleMusic()
        {
            _musicEnabled = !_musicEnabled;
            SetMusicVolume(_musicEnabled ? OnVolume : OffVolume);
        }

        public void PlayClip(AudioClip clip)
        {
            _audioOneShotPlayer.PlayOneShot(clip);
        }

        public bool IsMusicEnabled() => _musicEnabled;

        public bool IsSFXEnabled() => _sfxEnabled;

        private void SetMusicVolume(float volume)
        {
            _audioMixer.SetFloat(Music, volume);
        }

        private void SetSFXVolume(float volume)
        {
            _audioMixer.SetFloat(SFX, volume);
        }
    }
}