using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Transform _bulletSpawnPoint;

        private IBulletSpawner _bulletSpawner;

        [Inject]
        private void Construct(IBulletSpawner bulletSpawner) => _bulletSpawner = bulletSpawner;

        public void Fire(Vector2 direction)
        {
            _bulletSpawner.SpawnBullet(_bulletSpawnPoint.position, direction, _bulletConfig);
        }
    }
}