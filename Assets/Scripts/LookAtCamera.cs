using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _cam;

    private void OnEnable()
    {
        _cam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.forward = _cam.transform.forward;
    }
}
