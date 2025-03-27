using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Core.Movement.Systems
{
    public class RotationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _rotationGroup;
        private readonly List<GameEntity> _buffer = new();

        public RotationSystem(GameContext gameContext)
        {
            _rotationGroup = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Rotation, GameMatcher.Direction, GameMatcher.RotationSpeed));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _rotationGroup.GetEntities(_buffer))
            {
                ApplyRotationInstant(entity);
            }
        }

        private static void ApplyRotationInstant(GameEntity entity)
        {
            Vector3 direction = entity.direction.value;
            entity.ReplaceRotation(direction);
        }
    }
}