using ClientContent;
using UnityEngine;

namespace SpawnContent
{
    public class ClientSpawner : MonoBehaviour
    {
        [SerializeField] private Client _clientPrefabs;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private int _spawnAmount;

        private ObjectPool<Client> _clientPool;

        public void Init()
        {
            _clientPool = new ObjectPool<Client>(_clientPrefabs, _spawnAmount, _container);
            _clientPool.EnableAutoExpand();
        }

        public Client SpawnRandomClient()
        {
            Client client = _clientPool.GetFirstObject();

            if (client == null)
            {
                client = Instantiate(_clientPrefabs, _container);
                SetPosition(client);
            }
            else
            {
                SetPosition(client);
                client.gameObject.SetActive(true);
            }

            return client;
        }

        private void SetPosition(Client client)
        {
            client.transform.position = _spawnPosition.position;
            client.transform.rotation = _spawnPosition.rotation;
        }
    }
}