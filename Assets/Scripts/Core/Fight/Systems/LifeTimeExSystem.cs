using System.Collections.Generic;
using Entitas;
using Services;

namespace Core.Fight.Systems
{
    public class LifeTimeExSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _lifeTimeGroup;
        private readonly List<GameEntity> _buffer = new();

        public LifeTimeExSystem(GameContext gameContext, ITimeService timeService)
        {
            _lifeTimeGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.LifeTime));
            _timeService = timeService;
        }

        public void Execute()
        {
            float deltaTime = _timeService.DeltaTime;

            foreach (GameEntity entity in _lifeTimeGroup.GetEntities(_buffer))
            {
                UpdateLifeTime(entity, deltaTime);
            }
        }

        private void UpdateLifeTime(GameEntity entity, float deltaTime)
        {
            float currentValue = entity.lifeTime.value;
            float newValue = currentValue - deltaTime;

            if (newValue <= 0)
            {
                entity.RemoveLifeTime();
                entity.isDestroyed = true;
                return;
            }

            entity.ReplaceLifeTime(newValue);
        }
    }
}