using UnityEngine;

namespace PlayerContent
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private PlayerAnimations _playerAnimations;

        private CharacterController _controller;
        private PlayerInputActions _inputActions;
        private Vector2 _moveInput;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputActions = new PlayerInputActions();
            _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }

        private void Update()
        {
            Vector3 move = new Vector3(_moveInput.x, 0f, _moveInput.y);

            _controller.Move(move * _moveSpeed * Time.deltaTime);

            if (move.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }

            // ноги
            _playerAnimations.UpdateMovement(move);

            // удар косой
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerAnimations.PlaySwing();
            }
        }
    }
}