using System.Collections.Generic;
using SOContent.Resources;
using UnityEngine;
using Resource = Resources.Resource;

namespace PlayerContent
{
    public class ResourceInteractor : MonoBehaviour
    {
        [SerializeField] private Transform _cutPoint;
        [SerializeField] private float _cutRadius = 1.5f;
        [SerializeField] private float _cutDistance = 2f;
        [SerializeField] private LayerMask _resourceLayer;
        [SerializeField] private PlayerAnimations _playerAnimations;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private float _speedSwing;
        [SerializeField] private Inventory _inventory;

        public bool IsCanCut { get; private set; } = true;

        private void OnEnable()
        {
            _playerInput.SwingInput += PlaySwing;
        }

        private void OnDisable()
        {
            _playerInput.SwingInput -= PlaySwing;
        }

        private void Start()
        {
            _playerAnimations.SetSpeedSwing(_speedSwing);
        }

        private void PlaySwing()
        {
            if (!IsCanCut)
                return;

            SetValueCanCut(false);
            _playerAnimations.PlaySwing();
        }

        public void HandleGrassCut()
        {
            CheckResourceNearby();
        }

        public void CheckResourceNearby()
        {
            Collider[] hits = Physics.OverlapSphere(_cutPoint.position, _cutRadius, _resourceLayer);
            List<ResourceConfig> resourceConfigs = new List<ResourceConfig>();

            foreach (var hit in hits)
            {
                Debug.Log("hit " + hit.name);

                if (hit.TryGetComponent(out Resource resource))
                {
                    resource.Deactivate();
                    resourceConfigs.Add(resource.ResourceConfig);
                }
            }

            if (resourceConfigs.Count > 0)
            {
                foreach (var resourceConfig in resourceConfigs)
                    _inventory.AddResource(resourceConfig);
            }
        }

        public void CutGrass()
        {
        }

        public void SetValueCanCut(bool value)
        {
            IsCanCut = value;
        }

        private void OnDrawGizmos()
        {
            if (!_cutPoint) return;

            Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
            Gizmos.DrawSphere(_cutPoint.position, _cutRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_cutPoint.position, _cutPoint.position + transform.forward * _cutDistance);
            Gizmos.color = new Color(1f, 0f, 0f, 0.1f);
            Gizmos.DrawSphere(_cutPoint.position + transform.forward * _cutDistance, _cutRadius);
        }
    }
}