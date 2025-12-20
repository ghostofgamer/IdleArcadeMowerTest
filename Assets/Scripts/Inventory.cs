using System;
using System.Collections.Generic;
using AttentionContent;
using AudioContent;
using Enum;
using SOContent.Resources;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioClip _collectResourceClip;
    [SerializeField] private int _baseMaxAmount = 10;

    public int MaxAmount { get;private set; }
    private Dictionary<ResourcesType, int> _resources = new();

    public Action<int, int> ResourcesChanged;

    public int CurrentAmount => GetTotalAmount();
    public Dictionary<ResourcesType, int> Resources => _resources;

    private void Awake()
    {
        MaxAmount = _baseMaxAmount;
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

        if (CurrentAmount >= MaxAmount)
        {
            Debug.Log("У тебя переполнен инвентарь");
            AttentionHintActivator.ShowHint("У вас переполнен инвентарь");
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

        ResourcesChanged?.Invoke(CurrentAmount, MaxAmount);
    }

    public void ChangeMaxAmount(int amount)
    {
        MaxAmount = amount;
    }
}