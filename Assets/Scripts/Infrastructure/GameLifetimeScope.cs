using System.Collections.Generic;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform _worldContainer;
        [SerializeField] private Transform _poolsContainer;

        [Space][Header("Character")]
        [SerializeField] private GameObject _characterObject;
        [SerializeField] private MoveComponent _characterMovement;
        [SerializeField] private WeaponComponent _characterWeapon;
        [SerializeField] private HitPointsComponent _characterHitPoints;

        [Space][Header("Bullets Config")]
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletPoolWarmUpCount = 50;

        [Space][Header("Enemies Config")]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _maxActiveEnemiesCount = 7;
        [SerializeField] private int _enemySpawnCooldown = 1;
        [SerializeField] private PositionsGroup _enemySpawnPositions;
        [SerializeField] private PositionsGroup _enemyAttackPositions;

        [Space][Header("UI")]
        [SerializeField] private GameControlsView _gameControlsView;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCoreSystems(builder);
            RegisterCharacter(builder);
            RegisterBulletSystems(builder);
            RegisterEnemySystems(builder);
            RegisterUI(builder);

            // Force resolve non-lazy bindings
            builder.RegisterBuildCallback(container => container.Resolve<IEnumerable<INonLazy>>());
        }

        private void RegisterCoreSystems(IContainerBuilder builder)
        {
            builder.Register<TimeScaleService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GameManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterCharacter(IContainerBuilder builder)
        {
            builder.Register<CharacterMovementController>(Lifetime.Singleton)
                .WithParameter(_characterMovement)
                .AsImplementedInterfaces();

            builder.Register<CharacterFireController>(Lifetime.Singleton)
                .WithParameter(_characterWeapon)
                .AsImplementedInterfaces();

            builder.Register<CharacterDeathObserver>(Lifetime.Singleton)
                .WithParameter(_characterHitPoints)
                .AsImplementedInterfaces();
        }

        private void RegisterBulletSystems(IContainerBuilder builder)
        {
            RegisterPool(builder, "BulletPool", _bulletPrefab, _bulletPoolWarmUpCount);

            builder.Register<BulletSpawner>(Lifetime.Singleton)
                .WithParameter(_worldContainer)
                .AsImplementedInterfaces();
        }

        private void RegisterEnemySystems(IContainerBuilder builder)
        {
            RegisterPool(builder, "EnemyPool", _enemyPrefab, _maxActiveEnemiesCount);

            builder.Register<CooldownEnemySpawner>(Lifetime.Singleton)
                .WithParameter(_worldContainer)
                .WithParameter(_enemySpawnCooldown)
                .WithParameter(_enemySpawnPositions)
                .AsImplementedInterfaces();

            builder.Register<EnemyActivator>(Lifetime.Singleton)
                .WithParameter(_characterObject)
                .WithParameter(_enemyAttackPositions)
                .AsImplementedInterfaces();

            builder.Register<EnemyManager>(Lifetime.Singleton)
                .WithParameter(_maxActiveEnemiesCount)
                .AsImplementedInterfaces();
        }

        private void RegisterUI(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameControlsView).AsImplementedInterfaces();
            builder.Register<GameControlsPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterPool<T>(IContainerBuilder builder, string poolName, T prefab, int warmUpCount)
            where T : Component, IReusable<T>
        {
            GameObject poolContainer = new(poolName);
            poolContainer.transform.SetParent(_poolsContainer);

            builder.Register<ObjectPool<T>>(Lifetime.Singleton)
                .WithParameter(prefab)
                .WithParameter(poolContainer.transform)
                .WithParameter(warmUpCount)
                .AsImplementedInterfaces();
        }
    }
}