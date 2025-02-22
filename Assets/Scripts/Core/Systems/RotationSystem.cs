using Engine.Services;
using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class RotationSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _rotationGroup;

        public RotationSystem(GameContext gameContext, ITimeService timeService)
        {
            _rotationGroup = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Rotation, GameMatcher.Direction, GameMatcher.RotationSpeed));
            _timeService = timeService;
        }

        public void Execute()
        {
            // TODO доделать плавное вращение?
            // float deltaTime = _timeService.DeltaTime;

            foreach (GameEntity entity in _rotationGroup.GetEntities())
            {
                ApplyRotationInstant(entity);
            }
        }

        private static void ApplyRotationInstant(GameEntity entity)
        {
            Vector3 direction = entity.direction.value;
            entity.ReplaceRotation(direction);
        }

        private static void ApplyRotationSteeringForce(GameEntity entity, float deltaTime)
        {
            Vector3 currentRotation = entity.rotation.value;
            Vector3 direction = entity.direction.value;
            float rotationSpeed = entity.rotationSpeed.value;

            Vector3 steeringForce = (direction - currentRotation).normalized;
            steeringForce *= rotationSpeed * deltaTime;
            Vector3 newRotation = currentRotation + steeringForce;
            entity.ReplaceRotation(newRotation);
        }

        private static void ApplyRotationLerp(GameEntity entity, float deltaTime)
        {
            Vector3 currentRotation = entity.rotation.value;
            Vector3 direction = entity.direction.value;
            float rotationSpeed = entity.rotationSpeed.value;

            Vector3 newRotation = Vector3.Lerp(currentRotation, direction, rotationSpeed * deltaTime);
            entity.ReplaceRotation(newRotation);
        }
    }
}