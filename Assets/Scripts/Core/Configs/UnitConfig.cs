using UnityEngine;

namespace Core.Configs
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "Configs/UnitConfig")]
    public class UnitConfig : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _attackDistance;
        [SerializeField] private string _assetName;
        [SerializeField] private int _health;
        [SerializeField] private float _attackCooldown;

        public float MovementSpeed => _movementSpeed;

        public float RotationSpeed => _rotationSpeed;

        public float AttackDistance => _attackDistance;

        public string AssetName => _assetName;

        public int Health => _health;

        public float AttackCooldown => _attackCooldown;
    }
}