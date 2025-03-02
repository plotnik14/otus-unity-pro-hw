using Entitas;
using Services;
using UnityEngine;

namespace Core.Systems
{
    public class MovementSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _movementGroup;

        public MovementSystem(GameContext gameContext, ITimeService timeService)
        {
            _movementGroup = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Position, GameMatcher.Direction, GameMatcher.MovementSpeed)
                .NoneOf(GameMatcher.Target)
            );
            _timeService = timeService;
        }

        public void Execute()
        {
            float deltaTime = _timeService.DeltaTime;

            foreach (GameEntity entity in _movementGroup.GetEntities())
            {
                ApplyMovement(entity, deltaTime);
            }
        }

        private static void ApplyMovement(GameEntity entity, float deltaTime)
        {
            Vector3 currentPosition = entity.position.value;
            Vector3 direction = entity.direction.value.normalized;
            float speed = entity.movementSpeed.value;

            Vector3 newPosition = currentPosition + direction * (speed * deltaTime);
            entity.ReplacePosition(newPosition);
        }
    }
}