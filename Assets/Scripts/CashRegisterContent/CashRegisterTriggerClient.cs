using ClientContent;
using PlayerContent;
using UnityEngine;

namespace CashRegisterContent
{
    public class CashRegisterTriggerClient : MonoBehaviour
    {
        [SerializeField] private CashRegister _cashRegister;
        [SerializeField] private bool _cashRegisterTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (!_cashRegisterTriggered)
                if (other.TryGetComponent(out Client client))
                    _cashRegister.HandleClientReachedCounter(client);

            if (_cashRegisterTriggered)
                if (other.TryGetComponent(out Player player))
                    _cashRegister.HandleClient(player);
        }
    }
}