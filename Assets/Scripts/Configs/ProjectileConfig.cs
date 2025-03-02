using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/ProjectileConfig")]
    public class ProjectileConfig : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private string _assetName;

        public float MovementSpeed => _movementSpeed;

        public float Damage => _damage;

        public string AssetName => _assetName;
    }
}