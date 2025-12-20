using System;
using System.Collections.Generic;
using AudioContent;
using Enum;
using SOContent.Resources;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioClip _collectResourceClip;
    [SerializeField] private int _baseMaxAmount = 10;

    private int _maxAmount;
    private Dictionary<ResourcesType, int> _resources = new();

    public Action ResourcesChanged;
    
    public int CurrentAmount => GetTotalAmount();
    public Dictionary<ResourcesType, int> Resources => _resources;

    private void Start()
    {
        _maxAmount = _baseMaxAmount;
    }

    private int GetTotalAmount()
    {
        int total = 0;

        foreach (var value in _resources.Values)
            total += value;

        return total;
    }

    public void AddResource(ResourceConfig resourceConfig)
    {
        Debug.Log("Before add: " + CurrentAmount);

        if (CurrentAmount >= _maxAmount)
        {
            Debug.Log("У тебя переполнен инвентарь");
            return;
        }

        if (_resources.ContainsKey(resourceConfig.ResourceType))
            _resources[resourceConfig.ResourceType] += resourceConfig.Amount;
        else
            _resources[resourceConfig.ResourceType] = resourceConfig.Amount;

        AudioService.Instance.PlayClip(_collectResourceClip);

        Debug.Log("After add: " + CurrentAmount);

        foreach (var pair in _resources)
            Debug.Log($"{pair.Key} = {pair.Value}");
        
        ResourcesChanged?.Invoke();
    }

    public void ChangeMaxAmount(int amount)
    {
        _maxAmount = amount;
    }
}