using System.Collections.Generic;
using Entitas;

namespace Core.Fight.Systems
{
    public class AttackSystem : ReactiveSystem<GameEntity>
    {
        public AttackSystem(IContext<GameEntity> context) : base(context) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Target)
                .NoneOf(GameMatcher.AttackCooldown, GameMatcher.Destroyed)
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTarget
                   && !entity.hasAttackCooldown
                   && !entity.isDestroyed;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.hasAttackRequest = true;
            }
        }
    }
}