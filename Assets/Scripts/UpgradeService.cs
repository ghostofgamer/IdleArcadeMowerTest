using AttentionContent;
using PlayerContent;
using UnityEngine;

public class UpgradeService : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ImproverZoneTrigger _inventoryUpgradeZone;
    [SerializeField] private PlayerEffectHandler _playerEffectHandler;

    private void OnEnable()
    {
        _inventoryUpgradeZone.Triggered += TryUpgradeInventory;
    }

    private void OnDisable()
    {
        _inventoryUpgradeZone.Triggered -= TryUpgradeInventory;
    }

    private void TryUpgradeInventory()
    {
        if (!_inventory.CanLevelUp())
        {
            AttentionHintActivator.ShowHint("Инвентарь прокачен на максимум");
            return;
        }

        var nextLevel = _inventory.GetNextLevel();

        if (!_wallet.CanSpend(nextLevel.PriceCurrency, nextLevel.Price))
        {
            AttentionHintActivator.ShowHint($"Недостаточно валюты: {nextLevel.Price}");
            return;
        }

        _wallet.Spend(nextLevel.PriceCurrency, nextLevel.Price);
        _inventory.ApplyUpgrade();

        _playerEffectHandler.PlayEffectUpgrade();
    }
}