using System.Collections.Generic;
using Data;
using Player;
using UniRx;

namespace UI.PlayerPopup.Presenter
{
    public class StatsGroupPresenter : IStatsGroupPresenter
    {
        private readonly IPlayerInfo _playerInfo;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly ReactiveProperty<IReadOnlyList<Stat>> _stats = new();

        public IReadOnlyReactiveProperty<IReadOnlyList<Stat>> Stats => _stats;

        public StatsGroupPresenter(IPlayerInfo playerInfo)
        {
            _playerInfo = playerInfo;
            _playerInfo.CurrentLevel.Subscribe(OnLevelUp).AddTo(_compositeDisposable);
        }

        public void Dispose() => _compositeDisposable.Dispose();

        private void OnLevelUp(int _) => _stats.Value = _playerInfo.StatsForCurrentLevel;
    }
}