using System.Collections.Generic;
using Entitas;

namespace Core.Fight.Systems
{
    public class AttackRequestCleanupSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _attackRequestsGroup;
        private readonly List<GameEntity> _buffer = new();

        public AttackRequestCleanupSystem(GameContext gameContext)
        {
            _attackRequestsGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.AttackRequest));
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _attackRequestsGroup.GetEntities(_buffer))
            {
                entity.hasAttackRequest = false;
            }
        }
    }
}