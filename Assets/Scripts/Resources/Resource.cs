using System.Collections;
using AudioContent;
using SOContent.Resources;
using UnityEngine;

namespace Resources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceConfig _resourceConfig;
        [SerializeField] private GameObject _objectTarget;
        [SerializeField] private Collider _collider;
        [SerializeField]private ParticleSystem _hitEffect;

        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds;

        public ResourceConfig ResourceConfig => _resourceConfig;

        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(_resourceConfig.RespawnTime);
        }

        public void Deactivate()
        {
            _objectTarget.SetActive(false);
            _collider.enabled = false;
            AudioService.Instance.PlayClip(_resourceConfig.AudioClip);
            
            if (_hitEffect != null)
                _hitEffect.Play();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(RespawnCoroutine());
        }

        private IEnumerator RespawnCoroutine()
        {
            yield return _waitForSeconds;
            Activate();
        }

        private void Activate()
        {
            _collider.enabled = true;
            _objectTarget.SetActive(true);
        }
    }
}