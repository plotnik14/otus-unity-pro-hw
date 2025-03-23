using System.Collections.Generic;
using Entitas;

namespace Core.Systems
{
    public class DieRequestCleanup : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _dieRequestsGroup;
        private readonly List<GameEntity> _buffer = new();

        public DieRequestCleanup(GameContext gameContext)
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