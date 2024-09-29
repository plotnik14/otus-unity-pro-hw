using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] private PhysicsLayer _physicsLayer;
        [SerializeField] private Color _color;
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        public PhysicsLayer PhysicsLayer => _physicsLayer;
        public Color Color => _color;
        public int Damage => _damage;
        public float Speed => _speed;
    }
}