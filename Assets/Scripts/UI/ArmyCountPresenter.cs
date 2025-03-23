using System;

namespace UI
{
    public class ArmyCountPresenter : IArmyCountListener, IDisposable
    {
        private readonly UiArmyCountView _view;
        private readonly GameStateEntity _gameStateEntity;

        public ArmyCountPresenter(UiArmyCountView view, GameStateEntity gameStateEntity)
        {
            _view = view;
            _gameStateEntity = gameStateEntity;
        }

        public void Show()
        {
            _gameStateEntity.AddArmyCountListener(this);
            _view.Activate();
        }

        public void Hide()
        {
            _gameStateEntity.RemoveArmyCountListener(this);
            _view.Deactivate();
        }

        public void OnArmyCount(GameStateEntity entity, int value) => _view.SetCount(value);

        public void Dispose() => _gameStateEntity.RemoveArmyCountListener(this);
    }
}