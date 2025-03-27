using UnityEngine;

namespace Core.Configs
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/ProjectileConfig")]
    public class ProjectileConfig : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private float _lifeTime;
        [SerializeField] private string _assetName;

        public float MovementSpeed => _movementSpeed;

        public float Damage => _damage;

        public float LifeTime => _lifeTime;

        public string AssetName => _assetName;
    }
}