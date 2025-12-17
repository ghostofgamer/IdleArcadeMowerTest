using Enum;
using UnityEngine;

namespace SOContent.Resources
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "SOContent/ResourceConfig")]
    public class ResourceConfig : ScriptableObject
    {
        [SerializeField] private ResourcesType _resourceType;
        [SerializeField] private int _price;
        [SerializeField]private float _respawnTime;

        public ResourcesType ResourceType => _resourceType;
        public int Price => _price;
        public float RespawnTime => _respawnTime;
    }
}