using System.Collections.Generic;
using SOContent.CurrencyContent;
using UI;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _startValue;
    [SerializeField] private Currency[] _currencies;
    [SerializeField]private CurrencyUI _currencyUIPrefab;
    [SerializeField] private Transform _currencyUIParent;

    private Dictionary<Currency, int> _balances;
    private Dictionary<Currency, CurrencyUI> _uiMap;
    
    public void Init()
    {
        _balances = new Dictionary<Currency, int>(_currencies.Length);
        _uiMap = new Dictionary<Currency, CurrencyUI>(_currencies.Length);

        foreach (var currency in _currencies)
        {
            _balances.Add(currency, _startValue);

            var ui = Instantiate(_currencyUIPrefab, _currencyUIParent);
            ui.Init(currency, _startValue);

            _uiMap.Add(currency, ui);
        }
    }
    
    public void Add(Currency currency, int amount)
    {
        if (!_balances.ContainsKey(currency))
        {
            Debug.LogError($"Wallet: Currency {currency.name} not registered");
            return;
        }

        _balances[currency] += amount;
        _uiMap[currency].SetAmount(_balances[currency]);
    }

    public int GetBalance(Currency currency)
    {
        return _balances.TryGetValue(currency, out var value) ? value : 0;
    }
    
    public bool CanSpend(Currency currency, int amount)
    {
        return _balances.TryGetValue(currency, out var value) && value >= amount;
    }
    
    public bool Spend(Currency currency, int amount)
    {
        if (!CanSpend(currency, amount))
            return false;

        _balances[currency] -= amount;
        _uiMap[currency].SetAmount(_balances[currency]);
        return true;
    }
}