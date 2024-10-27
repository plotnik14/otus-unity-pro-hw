using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : IBulletSpawner, INonLazy
    {
        private readonly Transform _worldContainer;
        private readonly IObjectPool<Bullet> _pool;

        public BulletSpawner(Transform worldContainer, IObjectPool<Bullet> pool)
        {
            _worldContainer = worldContainer;
            _pool = pool;
        }

        public Bullet SpawnBullet(Vector2 position, Vector2 direction, BulletConfig bulletConfig)
        {
            Bullet bullet = _pool.Get();
            bullet.transform.SetParent(_worldContainer);
            bullet.gameObject.SetActive(true);
            bullet.Initialize(bulletConfig);
            bullet.Launch(position, direction);
            return bullet;
        }
    }
}