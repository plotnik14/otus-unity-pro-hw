using Engine.Services;
using Entitas;

namespace Core.Systems
{
    public class AttackCooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _cooldownGroup;

        public AttackCooldownSystem(GameContext gameContext, ITimeService timeService)
        {
            _cooldownGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.AttackCooldown));
            _timeService = timeService;
        }

        public void Execute()
        {
            float deltaTime = _timeService.DeltaTime;

            foreach (GameEntity entity in _cooldownGroup.GetEntities())
            {
                UpdateCooldown(entity, deltaTime);
            }
        }

        private void UpdateCooldown(GameEntity entity, float deltaTime)
        {
            float currentValue = entity.attackCooldown.value;
            float newValue = currentValue - deltaTime;

            if (newValue <= 0)
            {
                entity.RemoveAttackCooldown();
                return;
            }

            entity.ReplaceAttackCooldown(newValue);
        }
    }
}