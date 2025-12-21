using AudioContent;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerEffectHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effectUpgrade;
        [SerializeField]private AudioClip _audioClipUpgrade;

        public void PlayEffectUpgrade()
        {
            _effectUpgrade.Play();
            AudioService.Instance.PlayClip(_audioClipUpgrade);
        }
    }
}