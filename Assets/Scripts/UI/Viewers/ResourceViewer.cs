using UnityEngine;

namespace UI.Viewers
{
    public class ResourceViewer : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _content;
        [SerializeField] private ResourceUI[] _resources;
        
        private void OnEnable()
        {
            _inventory.ResourcesChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            _inventory.ResourcesChanged -= UpdateView;
        }
        
        private void UpdateView()
        {
            foreach (var ui in _resources)
            {
                int amount = _inventory.Resources.TryGetValue(ui.ResourceType, out var value) ? value : 0;
                ui.SetAmount(amount);
            }
        }
    }
}