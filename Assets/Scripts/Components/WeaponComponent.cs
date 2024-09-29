using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Transform _bulletSpawnPoint;

        private BulletManager _bulletManager;

        [UsedImplicitly]
        private void Awake()
        {
            // TODO получение ссылки через DI
            _bulletManager = FindObjectOfType<BulletManager>();
        }

        public void Fire(Vector2 direction)
        {
            _bulletManager.InstantiateBullet(_bulletSpawnPoint.position, direction, _bulletConfig);
        }
    }
}