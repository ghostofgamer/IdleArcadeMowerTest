using AttentionContent;
using ClientContent;
using Enum;
using PlayerContent;
using UnityEngine;

namespace CashRegisterContent
{
    public class CashRegister : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private ClientQueue _clientQueue; // ссылка на очередь
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Wallet _wallet;

        private Client _currentClientAtCounter;

        public void HandleClientReachedCounter(Client client)
        {
            if (_clientQueue.GetFirstClient() == client)
            {
                _target.SetActive(true);
                _currentClientAtCounter = client;
            }
        }

        public void HandleClientLeft(Client client)
        {
            if (_currentClientAtCounter == client)
            {
                _target.SetActive(false);
                _currentClientAtCounter = null;
            }
        }

        public void HandleClient(Player player)
        {
            if (_currentClientAtCounter == null)
            {
                AttentionHintActivator.ShowHint($"Нету клиента за стойкой");
                return;
            }

            ResourcesType wantedResource = _currentClientAtCounter.GetResourceType();

            int amount = _inventory.GetAmount(wantedResource);
            Debug.Log("amount " + amount);

            if (amount > 0)
            {
                Debug.Log("_inventory.Resources.TryGetValue ");
                _wallet.Add(_currentClientAtCounter.GetResource().ResourceConfig.Currency,
                    _currentClientAtCounter.GetResource().ResourceConfig.Price);
                _inventory.Resources[wantedResource] -= 1;
                _inventory.ResourcesChanged?.Invoke(_inventory.CurrentAmount, _inventory.MaxAmount);
                _currentClientAtCounter.CompleteOrder();
                HandleClientLeft(_currentClientAtCounter);
            }
            else
            {
                AttentionHintActivator.ShowHint(
                    $"У вас нет нужного ресурса для этого клиента , он хочет {_currentClientAtCounter.GetResourceType()}");
            }
        }
    }
}