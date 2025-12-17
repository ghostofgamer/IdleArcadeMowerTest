using System.Collections;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [SerializeField] private GameObject _grass;
    [SerializeField] private float _respawnTime = 5f;
    [SerializeField]private BoxCollider _collider;

    private Coroutine _coroutine;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_respawnTime);
    }

    public void Deactivate()
    {
        _grass.SetActive(false);
        _collider.enabled = false;
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return _waitForSeconds;
        Activate();
    }

    private void Activate()
    {
        _collider.enabled = true;
        _grass.SetActive(true);
    }
}