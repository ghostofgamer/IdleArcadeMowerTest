using UnityEngine;

namespace CameraContent
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);
        [SerializeField] private float _smoothSpeed = 5f;

        private Vector3 _desiredPosition;
        
        private void LateUpdate()
        {
            if (!_target)
                return;

            _desiredPosition = _target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed * Time.deltaTime);
        }
    }
}