using System;
using System.Collections;
using Enum;
using Resources;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace ClientContent
{
    public class Client : MonoBehaviour
    {
        [SerializeField] private Resource[] _resources;


        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private NavMeshObstacle _meshObstacle;
        // [SerializeField] private Animator _animator;

        [SerializeField] private Collider _clientCollider;

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

        public void Init(Vector3 startPosition, Vector3 queuePosition, Vector3 exitPosition)
        {
            transform.position = startPosition;
            _targetPosition = queuePosition;
            _isMoving = true;
            _exitPosition = exitPosition;
            int randomIndex = Random.Range(0, _resources.Length);
            _resource = _resources[randomIndex];
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
                yield break; // если не на NavMesh — просто выходим

            _navMeshAgent.speed = _moveSpeed;
            _navMeshAgent.SetDestination(position);

            while (_navMeshAgent.pathPending)
                yield return null;

            while (_navMeshAgent.remainingDistance > 0.05f)
                yield return null;

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
            OnLeaveStarted?.Invoke(this);   // 🔥 СРАЗУ
            SetDestination(_exitPosition, Leave);
        }

        public void Leave() => Exit?.Invoke();
    }
}