using UniRx;
using UnityEngine;

namespace UI.PlayerPopup.Presenter
{
    public interface IPlayerInfoPresenter
    {
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<int> Level { get; }
        IXpBarPresenter XpBarPresenter { get; }
    }
}