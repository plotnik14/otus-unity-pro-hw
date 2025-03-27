using System.Collections.Generic;
using Entitas;

namespace Core.Fight.Systems
{
    public class DieRequestCleanupSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _dieRequestsGroup;
        private readonly List<GameEntity> _buffer = new();

        public DieRequestCleanupSystem(GameContext gameContext)
        {
            _dieRequestsGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.DieRequest));
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _dieRequestsGroup.GetEntities(_buffer))
            {
                entity.hasDieRequest = false;
            }
        }
    }
}