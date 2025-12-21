using System;
using AttentionContent;
using PlayerContent;
using SOContent.CurrencyContent;
using UnityEngine;

public class UpgradeService : MonoBehaviour
{
    [SerializeField] private Sickle _sickle;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ImproverZoneTrigger _inventoryUpgradeZone;
    [SerializeField] private ImproverZoneTrigger _sickleUpgradeZone;
    [SerializeField] private PlayerEffectHandler _playerEffectHandler;

    private void OnEnable()
    {
        _inventoryUpgradeZone.Triggered += TryUpgradeInventory;
        _sickleUpgradeZone.Triggered += TrySickleUpgrade;
    }

    private void OnDisable()
    {
        _inventoryUpgradeZone.Triggered -= TryUpgradeInventory;
        _sickleUpgradeZone.Triggered -= TrySickleUpgrade;
    }

    private void TryUpgradeInventory()
    {
        if (!_inventory.CanLevelUp())
        {
            AttentionHintActivator.ShowHint("Инвентарь прокачен на максимум");
            return;
        }

        var nextLevel = _inventory.GetNextLevel();

        if (!TrySpend(nextLevel.PriceCurrency, nextLevel.Price))
            return;

        Spend(nextLevel.PriceCurrency, nextLevel.Price, () => _inventory.ApplyUpgrade());
    }

    private void TrySickleUpgrade()
    {
        if (!_sickle.CanLevelUp())
        {
            AttentionHintActivator.ShowHint("Серп прокачен на максимум");
            return;
        }

        var nextLevel = _sickle.GetNextLevel();

        if (!TrySpend(nextLevel.PriceCurrency, nextLevel.Price))
            return;

        Spend(nextLevel.PriceCurrency, nextLevel.Price, () => _sickle.ApplyUpgrade());
    }

    private bool TrySpend(Currency priceCurrency, int price)
    {
        if (_wallet.CanSpend(priceCurrency, price))
            return true;

        AttentionHintActivator.ShowHint($"Требуется {priceCurrency.Name}: {price}");

        return false;
    }

    private void Spend(Currency currency, int price, Action callback)
    {
        _wallet.Spend(currency, price);
        _playerEffectHandler.PlayEffectUpgrade();

        if (callback != null)
            callback?.Invoke();
    }
}