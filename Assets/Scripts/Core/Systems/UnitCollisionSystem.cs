using System.Collections.Generic;
using Entitas;

namespace Core.Systems
{
    public class UnitCollisionSystem : ReactiveSystem<GameEntity>
    {
        public UnitCollisionSystem(IContext<GameEntity> context) : base(context) { }

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
            // var damageValue = collidedWith.damage.value; // TODO Забирать из конфига
            float damage = 1f;

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