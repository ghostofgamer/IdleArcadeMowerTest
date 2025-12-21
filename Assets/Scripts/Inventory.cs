using System;
using System.Collections.Generic;
using AttentionContent;
using AudioContent;
using Enum;
using SOContent.Inventory;
using SOContent.Resources;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioClip _collectResourceClip;
    [SerializeField] private ImproverZoneTrigger _improverZoneTrigger;
    [SerializeField] private InventoryConfig _config;

    private Dictionary<ResourcesType, int> _resources = new();
    private int _currentLevel = 1;

    public Action<int, int> ResourcesChanged;

    private int _maxLevel => _config.Levels.Length;
    public int MaxAmount { get; private set; }
    public int CurrentAmount => GetTotalAmount();
    public Dictionary<ResourcesType, int> Resources => _resources;

    private void Awake()
    {
        ApplyLevel();
    }

    private int GetTotalAmount()
    {
        int total = 0;

        foreach (var value in _resources.Values)
            total += value;

        return total;
    }

    private void ApplyLevel()
    {
        MaxAmount = _config.Levels[_currentLevel - 1].MaxCapacity;
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
    
    public InventoryLevel GetNextLevel()
    {
        return _config.Levels[_currentLevel]; 
    }
    
    public void ApplyUpgrade()
    {
        _currentLevel++;
        ApplyLevel();
        ResourcesChanged?.Invoke(CurrentAmount, MaxAmount);
    }

    public bool CanLevelUp()
    {
        return _currentLevel < _maxLevel;
    }
}