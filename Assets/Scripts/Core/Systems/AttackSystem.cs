using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class AttackSystem : IExecuteSystem
    {
        // ToDO вынести в конфиг?
        private const float ATTACK_COOLDOWN = 3f;

        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _attackGroup;

        public AttackSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
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

            entity.AddAttackCooldown(ATTACK_COOLDOWN);
        }

        private void SpawnProjectileEntity(Vector3 spawnPosition, Vector3 direction, ETeam team)
        {
            // ToDO Spawn в отдельную систему?

            const string PROJECTILE_ASSET_NAME = "Projectile"; // ToDO В конфиг
            const float PROJECTILE_SPEED = 5f; // ToDO В конфиг

            GameEntity projectileEntity = _gameContext.CreateEntity();
            projectileEntity.AddPosition(spawnPosition);
            projectileEntity.AddDirection(direction);
            projectileEntity.AddTeam(team);
            projectileEntity.AddAsset(PROJECTILE_ASSET_NAME);
            projectileEntity.AddMovementSpeed(PROJECTILE_SPEED);
            projectileEntity.isProjectile = true;
        }
    }
}