using System;
using System.Collections.Generic;
using Data;
using UniRx;

namespace UI.PlayerPopup.Presenter
{
    public interface IStatsGroupPresenter : IDisposable
    {
        IReadOnlyReactiveProperty<IReadOnlyList<Stat>> Stats { get; }
    }
}