using Entitas;
using Services;

namespace Core.Systems
{
    public class LifeTimeSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _lifeTimeGroup;

        public LifeTimeSystem(GameContext gameContext, ITimeService timeService)
        {
            _lifeTimeGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.LifeTime));
            _timeService = timeService;
        }

        public void Execute()
        {
            float deltaTime = _timeService.DeltaTime;

            foreach (GameEntity entity in _lifeTimeGroup.GetEntities())
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