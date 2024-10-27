using UnityEngine;

namespace ShootEmUp
{
    public interface IBulletSpawner
    {
        Bullet SpawnBullet(Vector2 position, Vector2 direction, BulletConfig bulletConfig);
    }
}