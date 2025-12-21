using TMPro;
using UnityEngine;

namespace UI.Viewers
{
    public class ResourceViewer : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _content;
        [SerializeField] private ResourceUI[] _resources;
        [SerializeField] private TMP_Text _currentValueText;

        private void OnEnable()
        {
            _inventory.ResourcesChanged += UpdateView;
            UpdateView(_inventory.CurrentAmount, _inventory.MaxAmount);
        }

        private void OnDisable()
        {
            _inventory.ResourcesChanged -= UpdateView;
        }

        private void Start()
        {
            UpdateView(_inventory.CurrentAmount, _inventory.MaxAmount);
        }

        private void UpdateView(int currentAmount, int maxAmount)
        {
            foreach (var ui in _resources)
            {
                int amount = _inventory.Resources.TryGetValue(ui.ResourceType, out var value) ? value : 0;
                ui.SetAmount(amount);
            }

            _currentValueText.text = $"{currentAmount}/{maxAmount}";
        }
    }
}