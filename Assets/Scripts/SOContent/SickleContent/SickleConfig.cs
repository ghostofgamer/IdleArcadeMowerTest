using SOContent.CurrencyContent;
using UnityEngine;

namespace SOContent.SickleContent
{
    [CreateAssetMenu(fileName = "SickleConfig", menuName = "SOContent/SickleConfig")]
    public class SickleConfig : ScriptableObject
    {
        [SerializeField] private SickleLevel[] _levels;
        
        public SickleLevel[] Levels => _levels;
    }
    
    [System.Serializable]
    public class SickleLevel
    {
        [Min(1)]
        [SerializeField] private int _level;

        [Header("Stats")]
        [SerializeField] private float _cutRadius = 1.5f;
        [SerializeField] private float _swingSpeed = 1f;

        [Header("Upgrade Cost")]
        [SerializeField] private int _price;
        [SerializeField] private Currency _priceCurrency;
        
        public int Level => _level;
        public float CutRadius => _cutRadius;
        public float SwingSpeed => _swingSpeed;
        public int Price => _price;
        public Currency PriceCurrency => _priceCurrency;
    }
}
