using System;
using ClientContent;
using UnityEngine;

namespace CashRegisterContent
{
    public class CashRegisterTriggerClient : MonoBehaviour
    {
        [SerializeField] private CashRegister _cashRegister;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Client client))
                _cashRegister.HandleClientReachedCounter(client);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Client client))
                _cashRegister.HandleClientLeft(client);
        }
    }
}