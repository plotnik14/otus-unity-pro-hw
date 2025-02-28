using System.Collections.Generic;
using Entitas;

namespace Core.Systems
{
    public class ReleaseTargetSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _hasTargetGroup;

        public ReleaseTargetSystem(IContext<GameEntity> context) : base(context)
        {
            _hasTargetGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Target));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Unit, GameMatcher.Destroyed));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isUnit && entity.isDestroyed;
        }

        protected override void Execute(List<GameEntity> diedUnitEntities)
        {
            List<GameEntity> entitiesToReleaseTarget = new();

            foreach (GameEntity hasTargetEntity in _hasTargetGroup)
            {
                GameEntity target = hasTargetEntity.target.value;

                if (diedUnitEntities.Contains(target))
                    entitiesToReleaseTarget.Add(hasTargetEntity);
            }

            foreach (GameEntity entity in entitiesToReleaseTarget)
            {
                entity.RemoveTarget();
            }
        }
    }
}