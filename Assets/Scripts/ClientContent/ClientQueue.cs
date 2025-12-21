using System;
using System.Collections;
using System.Collections.Generic;
using SpawnContent;
using UnityEngine;

namespace ClientContent
{
    public class ClientQueue : MonoBehaviour
    {
        [SerializeField] private ClientSpawner _spawner;
        [SerializeField] private int _maxActiveClients = 5;
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private Transform _queueStartPoint;
        [SerializeField] private Transform _exitPosition;
        [SerializeField] private float _queueSpacing = 2f;

        private Queue<Client> _queue = new();
        private List<Client> _activeClients = new();
        private Coroutine _spawnCoroutine;
        private WaitForSeconds _spawnWait;
        private bool _isSpawning = true;

        public event Action<Client> OnClientReachedCounter;
        public event Action<Client> OnClientLeft;
        
        public int ActiveCount => _activeClients.Count;

        private void Start()
        {
            _spawnWait = new WaitForSeconds(_spawnInterval);

            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);

            _spawnCoroutine = StartCoroutine(AutoSpawn());
        }

        private IEnumerator AutoSpawn()
        {
            while (_isSpawning)
            {
                yield return new WaitForSeconds(_spawnInterval);

                if (_activeClients.Count < _maxActiveClients)
                    SpawnClient();
            }
        }

        private void SpawnClient()
        {
            Client client = _spawner.SpawnRandomClient();
            _activeClients.Add(client);

            // Присваиваем номерку и вычисляем позицию сразу
            int queueNumber = _activeClients.Count; // первый будет 1, второй 2 и т.д.
            Vector3 targetPosition = GetQueuePosition(queueNumber);

            client.Init(client.transform.position, targetPosition, _exitPosition.position);
            client.SetQueueNumber(queueNumber);
            client.SetDestination(targetPosition, null);

            // Подписка на Exit
            Action onExit = null;
            onExit = () =>
            {
                RemoveClient(client);
                client.Exit -= onExit; // отписка после выполнения
            };
            client.Exit += onExit;
        }

        private void RemoveClient(Client client)
        {
            if (_activeClients.Contains(client))
            {
                _activeClients.Remove(client);
                client.gameObject.SetActive(false);

                for (int i = 0; i < _activeClients.Count; i++)
                {
                    Client c = _activeClients[i];
                    c.SetQueueNumber(i + 1); // первый всегда 1
                    Vector3 newPos = GetQueuePosition(i + 1);
                    c.SetDestination(newPos, null);
                }
            }
        }

        private int GetClientIndex(Client client)
        {
            int index = 0;
            foreach (var c in _activeClients)
            {
                if (c == client) return index;
                index++;
            }

            return -1;
        }

        private Vector3 GetQueuePosition(int index)
        {
            return _queueStartPoint.position + new Vector3(0, 0, -index * _queueSpacing);
        }
        
        public Client GetFirstClient()
        {
            if (_activeClients.Count > 0)
                return _activeClients[0];
            return null;
        }
    }
}