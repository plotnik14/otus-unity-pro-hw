using Player;
using UniRx;
using UnityEngine;

namespace UI.PlayerPopup.Presenter
{
    public class PlayerInfoPresenter : IPlayerInfoPresenter
    {
        private readonly IPlayerInfo _playerInfo;

        public IReadOnlyReactiveProperty<Sprite> Icon => _playerInfo.Icon;
        public IReadOnlyReactiveProperty<string> Description => _playerInfo.Description;
        public IReadOnlyReactiveProperty<int> Level => _playerInfo.CurrentLevel;
        public IXpBarPresenter XpBarPresenter { get; }
        public string LevelLabel => "Level"; // localized value

        public PlayerInfoPresenter(IPlayerInfo playerInfo, IXpBarPresenter xpBarPresenter)
        {
            _playerInfo = playerInfo;
            XpBarPresenter = xpBarPresenter;
        }
    }
}