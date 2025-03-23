using System.Collections.Generic;
using Configs;
using Entitas;

namespace GameState.Systems
{
    public class UpdateArmyCountSystem : ReactiveSystem<GameStateEntity>
    {
        private readonly IGroup<GameEntity> _aliveArmy;
        private readonly List<GameEntity> _aliveArmyBuffer = new();

        public UpdateArmyCountSystem(
            IContext<GameStateEntity> gameStateContext,
            IContext<GameEntity> gameContext) : base(gameStateContext)
        {
            _aliveArmy = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Unit)
                .NoneOf(GameMatcher.DieRequest, GameMatcher.Destroyed)
            );
        }

        protected override ICollector<GameStateEntity> GetTrigger(IContext<GameStateEntity> context)
        {
            return context.CreateCollector(GameStateMatcher.UpdateArmyCountRequest.Added());
        }

        protected override bool Filter(GameStateEntity entity)
        {
            return entity.hasArmyCount && entity.hasUpdateArmyCountRequest;
        }

        protected override void Execute(List<GameStateEntity> entities)
        {
            foreach (GameStateEntity entity in entities)
            {
                // ToDo какая то херня. Надо подумать и почитать еще. Индексы?

                ETeam stateTeam = entity.stateTeam.value;
                int aliveCount = 0;


                // TODO
                // var gameEntities = _aliveArmy.GetEntities(_aliveArmyBuffer);
                // var gameEntities = Contexts.sharedInstance.game.GetGroup(GameMatcher
                //     .AllOf(GameMatcher.Unit)
                //     .NoneOf(GameMatcher.DieRequest, GameMatcher.Destroyed)
                // ).GetEntities(_aliveArmyBuffer);


                foreach (GameEntity gameEntity in _aliveArmy.GetEntities(_aliveArmyBuffer))
                {
                    if (gameEntity.team.value == stateTeam)
                        aliveCount++;
                }

                entity.ReplaceArmyCount(aliveCount);
                entity.hasUpdateArmyCountRequest = false;
            }
        }
    }
}