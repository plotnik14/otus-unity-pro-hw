using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private Transform _worldContainer;
        [SerializeField] private int _poolWarmUpCount = 50;

        private readonly List<GameObject> _activeBullets = new();
        private GameObjectPool _pool;

        [UsedImplicitly]
        private void Awake()
        {
            _pool = new GameObjectPool(_bulletPrefab.gameObject, _poolContainer, _poolWarmUpCount);
        }

        public void InstantiateBullet(Vector2 position, Vector2 direction, BulletConfig bulletConfig)
        {
            GameObject bulletObject = SpawnBulletObject();
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.OnCollisionEntered += OnBulletEnteredCollision;
            bullet.Initialize(bulletConfig);
            bullet.Launch(position, direction);
        }

        private GameObject SpawnBulletObject()
        {
            GameObject bulletObject = _pool.Get();
            bulletObject.transform.SetParent(_worldContainer);
            bulletObject.SetActive(true);
            _activeBullets.Add(bulletObject);
            return bulletObject;
        }

        private void OnBulletEnteredCollision(Bullet bullet, Collision2D collision)
        {
            bullet.OnCollisionEntered -= OnBulletEnteredCollision;
            GameObject bulletObject = bullet.gameObject;
            _activeBullets.Remove(bulletObject);
            _pool.Release(bulletObject);
        }
    }
}