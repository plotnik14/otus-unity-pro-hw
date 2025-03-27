using System.Collections.Generic;
using Core.Configs;
using Entitas;

namespace Core.Collisions.Systems
{
    public class UnitCollisionRxSystem : ReactiveSystem<GameEntity>
    {
        private readonly ProjectileConfig _projectileConfig;
        public UnitCollisionRxSystem(IContext<GameEntity> context, ProjectileConfig projectileConfig) : base(context)
        {
            _projectileConfig = projectileConfig;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Unit, GameMatcher.Collisions));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isUnit && entity.hasCollisions;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                ProcessCollisions(entity);
            }
        }

        private void ProcessCollisions(GameEntity entity)
        {
            List<GameEntity> collisionsList = entity.collisions.list;

            foreach (GameEntity collidedWith in collisionsList)
            {
                ProcessCollision(entity, collidedWith);
            }

            entity.RemoveCollisions();
        }

        private void ProcessCollision(GameEntity entity, GameEntity collidedWith)
        {
            if (!collidedWith.isProjectile)
                return;

            UpdateDamage(entity);
        }

        private void UpdateDamage(GameEntity entity)
        {
            float damage = _projectileConfig.Damage;

            if (entity.hasDamage)
            {
                float currentDamage = entity.damage.value;
                float newDamage = currentDamage + damage;
                entity.ReplaceDamage(newDamage);
                return;
            }

            entity.AddDamage(damage);
        }
    }
}