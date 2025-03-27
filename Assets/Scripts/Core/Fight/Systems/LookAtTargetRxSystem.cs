using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Core.Fight.Systems
{
    public class LookAtTargetRxSystem : ReactiveSystem<GameEntity>
    {
        public LookAtTargetRxSystem(IContext<GameEntity> context) : base(context) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.AttackRequest, GameMatcher.Target)
                .NoneOf(GameMatcher.Destroyed));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAttackRequest && entity.hasTarget;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                UpdateDirection(entity);
            }
        }

        private void UpdateDirection(GameEntity entity)
        {
            GameEntity targetEntity = entity.target.value;
            Vector3 targetPosition = targetEntity.position.value;
            Vector3 newDirection = (targetPosition - entity.position.value).normalized;
            entity.ReplaceDirection(newDirection);
        }
    }
}