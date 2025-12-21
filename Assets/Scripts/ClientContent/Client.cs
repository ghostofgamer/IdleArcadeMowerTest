using System;
using System.Collections;
using Enum;
using Resources;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace ClientContent
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private Resource[] _resources;
        [SerializeField] private Image _image;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private NavMeshObstacle _meshObstacle;
        [SerializeField] private Animator _animator;

        private Vector3 _targetPosition;
        private Vector3 _exitPosition;

        private float _moveSpeed = 6;
        private bool _isMoving = false;
        private Coroutine _moveCoroutine;
        private Resource _resource;

        public event Action<Client> OnReachedDestination;
        public event Action<Client> OnLeaveStarted;
        public event Action Exit;

        public int QueueNumber { get; private set; }

        private void Update()
        {
            float speed = _isMoving ? 1f : 0f;
            _animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
        }
        
        public void Init(Vector3 startPosition, Vector3 queuePosition, Vector3 exitPosition)
        {
            transform.position = startPosition;
            _targetPosition = queuePosition;
            _isMoving = true;
            _exitPosition = exitPosition;
            int randomIndex = Random.Range(0, _resources.Length);
            _resource = _resources[randomIndex];
            _image.sprite = _resource.ResourceConfig.Icon;
        }

        public Resource GetResource()
        {
            return _resource;
        }

        public ResourcesType GetResourceType()
        {
            return _resource.ResourceConfig.ResourceType; // _resource — объект Resource у клиента
        }

        public void SetDestination(Vector3 position, Action callback)
        {
            Debug.Log("SetDestination");

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(MoveToPosition(position, callback));
        }

        public void SetQueueNumber(int number)
        {
            QueueNumber = number;
        }

        private IEnumerator MoveToPosition(Vector3 position, Action callback)
        {
            _meshObstacle.enabled = false;

            if (!_navMeshAgent.enabled)
                _navMeshAgent.enabled = true;

            if (!_navMeshAgent.isOnNavMesh)
                yield break;

            _isMoving = true;
            _navMeshAgent.speed = _moveSpeed;
            _navMeshAgent.SetDestination(position);

            while (_navMeshAgent.pathPending)
                yield return null;

            while (_navMeshAgent.remainingDistance > 0.05f)
                yield return null;

            _isMoving = false;

            if (_navMeshAgent.enabled)
                _navMeshAgent.enabled = false;

            _meshObstacle.enabled = true;
            callback?.Invoke();
            OnReachedDestination?.Invoke(this);
        }

        [ContextMenu("CompleteOrder")]
        public void CompleteOrder()
        {
            // SetDestination(_exitPosition, Leave);
            OnLeaveStarted?.Invoke(this); // 🔥 СРАЗУ
            SetDestination(_exitPosition, Leave);
        }

        public void Leave() => Exit?.Invoke();
    }
}