using UnityEngine;

namespace CameraContent
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);
        [SerializeField] private float _smoothSpeed = 5f;

        private void LateUpdate()
        {
            if (!_target)
                return;

            Vector3 desiredPosition = _target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        }
    }
}