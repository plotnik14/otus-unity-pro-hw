using Engine.Configs;
using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class AttackSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly UnitConfig _unitConfig;
        private readonly ProjectileConfig _projectileConfig;
        private readonly IGroup<GameEntity> _attackGroup;

        public AttackSystem(
            GameContext gameContext,
            UnitConfig unitConfig,
            ProjectileConfig projectileConfig)
        {
            _gameContext = gameContext;
            _unitConfig = unitConfig;
            _projectileConfig = projectileConfig;

            _attackGroup = _gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Target)
                .NoneOf(GameMatcher.AttackCooldown)
            );
        }

        public void Execute()
        {
            foreach (GameEntity entity in _attackGroup.GetEntities())
            {
                PerformAttack(entity);
            }
        }

        private void PerformAttack(GameEntity entity)
        {
            // ToDo вынести в реквест?
            GameEntity targetEntity = entity.target.value;

            if (targetEntity.isDestroyed)
            {
                // ToDO не работает. Нужно починить сброс Target при смерти Target
                entity.RemoveTarget();
                return;
            }

            if (!targetEntity.hasPosition)
            {
                // ToDO убрать костыль
                entity.RemoveTarget();
                return;
            }

            Vector3 targetPosition = targetEntity.position.value;

            // ToDO поворот в сторону цели вынести в отдельную систему?
            entity.ReplaceDirection((targetPosition - entity.position.value).normalized);

            ETeam team = entity.team.value;
            Vector3 muzzlePosition = entity.position.value;
            Vector3 firePoint = muzzlePosition + entity.direction.value; // ToDO как правильно задать точку выстрела? Нужно как то забратб из вью
            Vector3 direction = (targetPosition - firePoint).normalized;
            SpawnProjectileEntity(firePoint, direction, team);

            entity.AddAttackCooldown(_unitConfig.AttackCooldown);
        }

        private void SpawnProjectileEntity(Vector3 spawnPosition, Vector3 direction, ETeam team)
        {
            // ToDO Spawn в отдельную систему?
            GameEntity projectileEntity = _gameContext.CreateEntity();
            projectileEntity.AddPosition(spawnPosition);
            projectileEntity.AddDirection(direction);
            projectileEntity.AddTeam(team);
            projectileEntity.AddAsset(_projectileConfig.AssetName);
            projectileEntity.AddMovementSpeed(_projectileConfig.MovementSpeed);
            projectileEntity.isProjectile = true;
        }
    }
}