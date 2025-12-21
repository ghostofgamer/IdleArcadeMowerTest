using SOContent.CurrencyContent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _valueText;

        private Currency _currency;

        public void Init(Currency currency, int startAmount)
        {
            _currency = currency;
            _icon.sprite = currency.Icon;
            SetAmount(startAmount);
        }

        public void SetAmount(int amount)
        {
            _valueText.text = amount.ToString();
        }
    }
}