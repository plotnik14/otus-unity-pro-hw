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

        private CompositeDisposable _compositeDisposable;

        public void Show(IStatsGroupPresenter statsGroupPresenter)
        {
            _compositeDisposable = new CompositeDisposable();
            statsGroupPresenter.Stats.Subscribe(OnStatsChanged).AddTo(_compositeDisposable);
        }

        public void Hide() => _compositeDisposable.Dispose();

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