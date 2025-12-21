using System;
using UnityEngine;

namespace PlayerContent
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInputActions _inputActions;

        public event Action<Vector2> MoveInput;
        public event Action SwingInput;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.Move.performed += ctx => MoveInput?.Invoke(ctx.ReadValue<Vector2>());
            _inputActions.Player.Move.canceled += ctx => MoveInput?.Invoke(Vector2.zero);
            _inputActions.Player.Swing.performed += ctx => SwingInput?.Invoke();
        }
    
        private void OnEnable()
        {
            _inputActions.Player.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Disable();
        }
    }
}