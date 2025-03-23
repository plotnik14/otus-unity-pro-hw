using System;
using System.Collections.Generic;
using Configs;
using Entitas;
using UnityEngine;
using View;

namespace Core.Systems
{
    public class SpawnProjectileSystem : ReactiveSystem<GameEntity>
    {
        private readonly IContext<GameEntity> _context;
        private readonly ProjectileConfig _projectileConfig;

        public SpawnProjectileSystem(IContext<GameEntity> context, ProjectileConfig projectileConfig) : base(context)
        {
            _context = context;
            _projectileConfig = projectileConfig;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Unit, GameMatcher.AttackRequest, GameMatcher.Target)
                .NoneOf(GameMatcher.Destroyed));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isUnit
                   && entity.hasAttackRequest
                   && entity.hasTarget;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                SpawnProjectile(entity);
            }
        }

        private void SpawnProjectile(GameEntity entity)
        {
            UnitView unitView = entity.entityView.value as UnitView;

            if (unitView is null)
                throw new InvalidOperationException("Failed to create projectile. Entity is not a UnitView");

            ETeam ownTeam = entity.team.value;
            Vector3 spawnPosition = unitView.FirePoint;
            GameEntity targetEntity = entity.target.value;
            Vector3 targetPosition = targetEntity.position.value;
            Vector3 direction = (targetPosition - spawnPosition).normalized;
            CreateProjectileEntity(spawnPosition, direction, ownTeam);
        }

        private void CreateProjectileEntity(Vector3 spawnPosition, Vector3 direction, ETeam team)
        {
            GameEntity projectileEntity = _context.CreateEntity();
            projectileEntity.AddPosition(spawnPosition);
            projectileEntity.AddDirection(direction);
            projectileEntity.AddTeam(team);
            projectileEntity.AddAsset(_projectileConfig.AssetName);
            projectileEntity.AddMovementSpeed(_projectileConfig.MovementSpeed);
            projectileEntity.AddLifeTime(_projectileConfig.LifeTime);
            projectileEntity.isProjectile = true;
        }
    }
}