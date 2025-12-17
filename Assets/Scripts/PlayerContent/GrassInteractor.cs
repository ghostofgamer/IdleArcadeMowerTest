using UnityEngine;

namespace PlayerContent
{
    public class GrassInteractor : MonoBehaviour
    {
        [Header("Cut Settings")] [SerializeField]
        private Transform cutPoint;

        [SerializeField] private float cutRadius = 1.5f;
        [SerializeField] private float cutDistance = 2f;
        [SerializeField] private LayerMask grassLayer;
        [SerializeField] [Range(0f, 1f)] private float forwardDotThreshold = 0.5f;
        [SerializeField] private PlayerAnimations _playerAnimations;
        [SerializeField]private PlayerInput _playerInput;

        [SerializeField] private float _speedSwing;
        
        public bool IsCanCut { get; private set; } = true;

        private void OnEnable()
        {
            _playerInput.SwingInput += PlaySwing;
        }

        private void OnDisable()
        {
            _playerInput.SwingInput -= PlaySwing;
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
            CheckGrassNearby();
        }

        private void Start()
        {
            _playerAnimations.SetSpeedSwing(_speedSwing);
        }
        
        public void CheckGrassNearby()
        {
            Collider[] hits = Physics.OverlapSphere(cutPoint.position, cutRadius, grassLayer);

            foreach (var hit in hits)
            {
                Debug.Log("hit " + hit.name);
                
                if (hit.TryGetComponent(out Grass grass))
                {
                    Debug.Log("Нашли " + grass.name);
                    grass.Deactivate();
                }
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
            if (!cutPoint) return;

            Gizmos.color = new Color(0f, 1f, 0f, 0.3f); 
            Gizmos.DrawSphere(cutPoint.position, cutRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(cutPoint.position, cutPoint.position + transform.forward * cutDistance);
            Gizmos.color = new Color(1f, 0f, 0f, 0.1f);
            Gizmos.DrawSphere(cutPoint.position + transform.forward * cutDistance, cutRadius);
        }
    }
}