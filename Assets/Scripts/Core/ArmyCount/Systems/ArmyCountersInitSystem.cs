using System;
using Core.Configs;
using Entitas;

namespace Core.ArmyCount.Systems
{
    public class ArmyCountersInitSystem : IInitializeSystem
    {
        private readonly GameStateContext _gameStateContext;

        public ArmyCountersInitSystem(GameStateContext gameStateContext) => _gameStateContext = gameStateContext;

        public void Initialize()
        {
            foreach (ETeam team in Enum.GetValues(typeof(ETeam)))
                CreateArmyCounter(team);
        }

        private void CreateArmyCounter(ETeam team)
        {
            GameStateEntity entity = _gameStateContext.CreateEntity();
            entity.AddStateTeam(team);
            entity.AddArmyCount(0);
        }
    }
}