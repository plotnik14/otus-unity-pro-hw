using System.Collections.Generic;
using Entitas;

namespace Core.Collisions.Systems
{
    public class ProjectileCollisionRxSystem : ReactiveSystem<GameEntity>
    {
        public ProjectileCollisionRxSystem(IContext<GameEntity> context) : base(context) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Projectile, GameMatcher.Collisions));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isProjectile && entity.hasCollisions;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                ProcessCollision(entity);
                entity.RemoveCollisions();
            }
        }

        private void ProcessCollision(GameEntity entity)
        {
            entity.isDestroyed = true;
        }
    }
}