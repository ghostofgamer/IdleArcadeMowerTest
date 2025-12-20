using UnityEngine;

public class UIRotator : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;

    private void Update()
    {
        _target.transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
    }
}