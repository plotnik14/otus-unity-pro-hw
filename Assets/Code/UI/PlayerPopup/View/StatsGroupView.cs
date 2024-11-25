using System.Collections.Generic;
using Data;
using UI.PlayerPopup.Presenter;
using UniRx;
using UnityEngine;

namespace UI.PlayerPopup.View
{
    public class StatsGroupView : MonoBehaviour
    {
        [SerializeField] private List<StatView> _statViewGroup;

        private readonly CompositeDisposable _compositeDisposable = new();

        public void Show(IStatsGroupPresenter statsGroupPresenter)
        {
            _compositeDisposable.Clear();
            statsGroupPresenter.Stats.Subscribe(OnStatsChanged).AddTo(_compositeDisposable);
        }

        public void Hide() => _compositeDisposable.Clear();

        private void OnStatsChanged(IReadOnlyList<Stat> stats)
        {
            for (int index = 0; index < _statViewGroup.Count; index++)
            {
                StatView statView = _statViewGroup[index];
                Stat stat = stats[index];
                statView.SetInfo(stat.Name, stat.Value);
            }
        }
    }
}