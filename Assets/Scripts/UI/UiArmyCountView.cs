using System;
using System.Linq;
using Configs;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UiArmyCountView : MonoBehaviour, IArmyCountListener
    {
        [SerializeField] private TMP_Text _count;
        [SerializeField] private ETeam _team;

        private GameStateEntity _gameStateEntity;

        [UsedImplicitly]
        private void Start()
        {
            DelayedInit().Forget();
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            _gameStateEntity.RemoveArmyCountListener(this);
        }

        public void OnArmyCount(GameStateEntity entity, int value)
        {
            if (entity.stateTeam.value == _team)
                _count.text = value.ToString();
        }

        private async UniTaskVoid DelayedInit()
        {
            await UniTask.DelayFrame(1);
            // ToDO переписать
            var stateEntities = Contexts.sharedInstance.gameState.GetEntities();
            _gameStateEntity = stateEntities.First(stateEntity => stateEntity.stateTeam.value == _team);
            _gameStateEntity.AddArmyCountListener(this);
            OnArmyCount(_gameStateEntity, _gameStateEntity.armyCount.value);
        }
    }
}