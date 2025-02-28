using System.Collections.Generic;
using Entitas;

namespace Core.Systems
{
    public class DestroyedCleanupSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _destroyedGroup;
        private readonly List<GameEntity> _buffer = new();

        public DestroyedCleanupSystem(GameContext gameContext)
        {
            _destroyedGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Destroyed));
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _destroyedGroup.GetEntities(_buffer))
            {
                entity.Destroy();
            }
        }
    }
}