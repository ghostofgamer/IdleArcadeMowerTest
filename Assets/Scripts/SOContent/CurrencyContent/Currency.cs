using Enum;
using UnityEngine;

namespace SOContent.CurrencyContent
{
    [CreateAssetMenu(fileName = "CurrencyConfig", menuName = "SOContent/Currency")]
    public class Currency : ScriptableObject
    {
        public CurrencyType CurrencyType; 
        public Sprite Icon;
        public string Name;
    }
}
