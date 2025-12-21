using ClientContent;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private ClientQueue _clientQueue;      // ссылка на очередь
    [SerializeField] private Transform _counterPosition; 
    
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
        // Выключаем объект, когда клиент уходит
        if (_currentClientAtCounter == client)
        {
            _target.SetActive(false);
            _currentClientAtCounter = null;
        }
    }
}