using System.Collections.Generic;
using Entitas;
using UI;

namespace GameState.Systems
{
    public class InitArmyCountersSystem : IInitializeSystem
    {
        private readonly GameStateContext _gameStateContext;
        private readonly List<UiArmyCountView> _armyCountViews;

        public InitArmyCountersSystem(GameStateContext gameStateContext, List<UiArmyCountView> armyCountViews)
        {
            _gameStateContext = gameStateContext;
            _armyCountViews = armyCountViews;
        }

        public void Initialize()
        {
            foreach (UiArmyCountView view in _armyCountViews)
                CreateArmyCounter(view);
        }

        private void CreateArmyCounter(UiArmyCountView view)
        {
            GameStateEntity entity = _gameStateContext.CreateEntity();
            entity.AddStateTeam(view.Team);
            entity.AddArmyCount(0);
            var armyCountPresenter = new ArmyCountPresenter(view, entity);
            entity.AddArmyCountPresenter(armyCountPresenter);
            armyCountPresenter.Show();
        }
    }
}