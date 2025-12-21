using UnityEngine;

namespace SOContent.PlayerContent
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "SOContent/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _speedMove;
        [SerializeField] private float _speedRotate;
        
        public float SpeedMove => _speedMove;
        public float SpeedRotate => _speedRotate;
    }
}
