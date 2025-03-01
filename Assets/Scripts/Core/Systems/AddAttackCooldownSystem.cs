using System.Collections.Generic;
using Engine.Configs;
using Entitas;

namespace Core.Systems
{
    public class AddAttackCooldownSystem : ReactiveSystem<GameEntity>
    {
        private readonly UnitConfig _unitConfig;

        public AddAttackCooldownSystem(IContext<GameEntity> context, UnitConfig unitConfig) : base(context)
        {
            _unitConfig = unitConfig;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.Unit, GameMatcher.AttackRequest));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isUnit && entity.hasAttackRequest;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                entity.AddAttackCooldown(_unitConfig.AttackCooldown);
            }
        }
    }
}