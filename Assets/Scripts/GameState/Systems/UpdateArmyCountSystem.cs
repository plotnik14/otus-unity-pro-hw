using System.Collections.Generic;
using Configs;
using Entitas;

namespace GameState.Systems
{
    public class UpdateArmyCountSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameStateEntity> _counterEntities;
        private readonly List<GameStateEntity> _counterBuffer = new();

        public UpdateArmyCountSystem(
            IContext<GameEntity> gameContext,
            IContext<GameStateEntity> gameStateContext) : base(gameContext)
        {
            _counterEntities = gameStateContext.GetGroup(GameStateMatcher
                .AllOf(GameStateMatcher.ArmyCount, GameStateMatcher.StateTeam));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Unit.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> entities)
        {
            if (entities.Count == 0)
                return;

            Dictionary<ETeam, GameStateEntity> teamStateDictionary = CreateTeamStateDictionary();

            foreach (GameEntity entity in entities)
            {
                ETeam team = entity.team.value;
                GameStateEntity counterEntity = teamStateDictionary[team];
                int armyCountCurrent = counterEntity.armyCount.value;
                bool hasUnitMark = entity.isUnit;
                int armyCountNew = hasUnitMark
                    ? ++armyCountCurrent
                    : --armyCountCurrent;
                counterEntity.ReplaceArmyCount(armyCountNew);
            }
        }

        private Dictionary<ETeam, GameStateEntity> CreateTeamStateDictionary()
        {
            Dictionary<ETeam, GameStateEntity> teamStateDictionary = new();

            foreach (GameStateEntity stateEntity in _counterEntities.GetEntities(_counterBuffer))
                teamStateDictionary.Add(stateEntity.stateTeam.value, stateEntity);

            return teamStateDictionary;
        }
    }
}