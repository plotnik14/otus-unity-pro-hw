using System.Collections.Generic;
using Entitas;

namespace Core.Fight.Systems
{
    public class UnitDieSystem : ReactiveSystem<GameEntity>
    {
        public UnitDieSystem(IContext<GameEntity> game) : base(game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Unit, GameMatcher.DieRequest)
                .NoneOf(GameMatcher.Destroyed)
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasDieRequest && !entity.isDestroyed;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.isUnit = false;
                entity.isDestroyed = true;
            }
        }
    }
}