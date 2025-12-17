using UnityEngine;

namespace PlayerContent
{
    public class PlayerAnimations : MonoBehaviour
    {
        private const string Speed = "Speed";
        private const string Swing = "Swing";

        [SerializeField] private Animator _animator;

        private float _dampTime = 0.1f;

        public void UpdateMovement(Vector3 move)
        {
            _animator.SetFloat(Speed, move.magnitude, _dampTime , Time.deltaTime);
        }

        public void PlaySwing()
        {
            _animator.SetTrigger(Swing);
        }
    }
}