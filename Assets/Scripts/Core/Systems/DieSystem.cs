using System.Collections.Generic;
using Configs;
using Entitas;

namespace Core.Systems
{
    public class DieSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameStateEntity> _armyCounters;
        private readonly List<GameStateEntity> _armyCountersBuffer = new();

        public DieSystem(IContext<GameEntity> game, IContext<GameStateEntity> gameState) : base(game)
        {
            _armyCounters = gameState.GetGroup(GameStateMatcher
                .AllOf(GameStateMatcher.ArmyCount, GameStateMatcher.StateTeam)
                .NoneOf(GameStateMatcher.UpdateArmyCountRequest)
            );
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(GameMatcher.DieRequest)
                .NoneOf(GameMatcher.Destroyed)
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasDieRequest;
            // return entity.hasDieRequest && !entity.isDestroyed; // TODO
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                ETeam team = entity.team.value;

                foreach (GameStateEntity gameStateEntity in _armyCounters.GetEntities(_armyCountersBuffer))
                {
                    ETeam counterTeam = gameStateEntity.stateTeam.value;
                    var hasUpdateArmyCount = gameStateEntity.hasUpdateArmyCountRequest;

                    if (team != counterTeam)
                        continue;

                    if (hasUpdateArmyCount)
                        continue;

                    gameStateEntity.hasUpdateArmyCountRequest = true;
                }
                // ToDO все что выше вынести в отдельную систему
                // ToDO CleanUpSystem


                entity.isDestroyed = true;
            }
        }
    }
}