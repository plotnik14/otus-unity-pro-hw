using Player;
using UniRx;

namespace UI.PlayerPopup.Presenter
{
    public class XpBarPresenter : IXpBarPresenter
    {
        private readonly IPlayerInfo _playerInfo;

        public IReadOnlyReactiveProperty<int> CurrentXpValue => _playerInfo.CurrentExperience;
        public int RequiredXpValue => _playerInfo.RequiredExpForLevelUp;
        public string XpLabel => "XP"; // localized value

        public XpBarPresenter(IPlayerInfo playerInfo) => _playerInfo = playerInfo;
    }
}