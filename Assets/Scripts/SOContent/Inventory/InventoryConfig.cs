using SOContent.CurrencyContent;
using UnityEngine;

namespace SOContent.Inventory
{
    [CreateAssetMenu(fileName = "InventoryConfig", menuName = "SOContent/Inventory Config")]
    public class InventoryConfig : ScriptableObject
    {
        public InventoryLevel[] Levels;
    }
    
    [System.Serializable]
    public class InventoryLevel
    {
        [Min(1)]
        public int Level;

        [Min(0)]
        public int MaxCapacity;

        public int Price;

        public Currency PriceCurrency;
    }
}
