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
            Vector3 targetPosition = targetEntity.position.value;
            ETeam team = entity.team.value;
            Vector3 muzzlePosition = entity.position.value; // ToDO как правильно задать точку выстрела?
            Vector3 direction = (targetPosition - muzzlePosition).normalized;
            SpawnProjectileEntity(muzzlePosition, direction, team);

            entity.AddAttackCooldown(ATTACK_COOLDOWN);
        }

        private void SpawnProjectileEntity(Vector3 spawnPosition, Vector3 direction, ETeam team)
        {
            const string PROJECTILE_ASSET_NAME = "Projectile"; // В конфиг
            const float PROJECTILE_SPEED = 1f; // В конфиг

            GameEntity projectileEntity = _gameContext.CreateEntity();
            projectileEntity.AddPosition(spawnPosition);
            projectileEntity.AddDirection(direction);
            projectileEntity.AddTeam(team);
            projectileEntity.AddAsset(PROJECTILE_ASSET_NAME);
            projectileEntity.AddMovementSpeed(PROJECTILE_SPEED);
        }
    }
}