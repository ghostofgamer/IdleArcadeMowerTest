using Enum;
using SOContent.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private ResourceConfig _resourceConfig;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Image _icon;

        public ResourcesType ResourceType => _resourceConfig.ResourceType;

        private void Start()
        {
            _icon.sprite = _resourceConfig.Icon;
        }

        public void SetAmount(int amount)
        {
            gameObject.SetActive(amount > 0);
            _amountText.text = amount.ToString();
        }
    }
}