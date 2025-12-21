using System.Collections.Generic;
using SOContent.Resources;
using UnityEngine;
using Resource = Resources.Resource;

namespace PlayerContent
{
    public class ResourceInteractor : MonoBehaviour
    {
        [SerializeField] private Sickle _sickle;
        [SerializeField] private Transform _cutPoint;
        [SerializeField] private float _cutDistance = 2f;
        [SerializeField] private LayerMask _resourceLayer;
        [SerializeField] private PlayerAnimations _playerAnimations;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Inventory _inventory;

        private int _currentlevel = 1;

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
            _playerAnimations.SetSpeedSwing(_sickle.SwingSpeed);
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

        private void CheckResourceNearby()
        {
            Collider[] hits = Physics.OverlapSphere(_cutPoint.position, _sickle.CutRadius, _resourceLayer);
            List<ResourceConfig> resourceConfigs = new List<ResourceConfig>();

            foreach (var hit in hits)
            {
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

        public void SetValueCanCut(bool value)
        {
            IsCanCut = value;
        }
    }
}