using System;
using Configs;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UiArmyStatisticsView : UiEntityView, IArmyCountListener
    {
        [SerializeField] private UiArmyCountView _uiArmyCountViewRed;
        [SerializeField] private UiArmyCountView _uiArmyCountViewBlue;

        private GameStateContext _gameStateContext;

        [Inject]
        private void Construct(GameStateContext gameStateContext) => _gameStateContext = gameStateContext;

        [UsedImplicitly]
        private void Start()
        {
            GameStateEntity[] armyCounters = _gameStateContext
                .GetGroup(GameStateMatcher.AllOf(GameStateMatcher.StateTeam, GameStateMatcher.ArmyCount))
                .GetEntities();

            foreach (GameStateEntity armyCounter in armyCounters)
            {
                armyCounter.AddArmyCountListener(this);
                int currentCount = armyCounter.armyCount.value;
                OnArmyCount(armyCounter, currentCount);
            }
        }

        public void OnArmyCount(GameStateEntity entity, int armyCount)
        {
            switch (entity.stateTeam.value)
            {
                case ETeam.BlueTeam:
                    _uiArmyCountViewBlue.SetCount(armyCount);
                    break;
                case ETeam.RedTeam:
                    _uiArmyCountViewRed.SetCount(armyCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}