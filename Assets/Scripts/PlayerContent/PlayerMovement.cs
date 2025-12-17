using UnityEngine;

namespace PlayerContent
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private PlayerAnimations _playerAnimations;
        [SerializeField] private PlayerInput _playerInput;

        private CharacterController _controller;
        private Vector2 _moveInput;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _playerInput.MoveInput += HandleMoveInput;
        }

        private void OnDisable()
        {
            _playerInput.MoveInput -= HandleMoveInput;
        }

        private void HandleMoveInput(Vector2 input)
        {
            _moveInput = input;
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

            _playerAnimations.UpdateMovement(move);
        }
    }
}