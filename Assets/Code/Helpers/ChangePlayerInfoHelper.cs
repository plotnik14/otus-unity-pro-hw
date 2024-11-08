using Player;
using Sirenix.OdinInspector;
using UI.PlayerPopup.Factory;
using UI.PlayerPopup.Presenter;
using UI.PlayerPopup.View;
using UnityEngine;
using Zenject;

namespace Helpers
{
    public class ChangePlayerInfoHelper : MonoBehaviour
    {
        private PlayerPopupView _view;
        private PlayerPopupPresenterFactory _presenterFactory;
        private IPlayerInfo _playerInfo;

        [Inject]
        public void Construct(
            PlayerPopupView view,
            PlayerPopupPresenterFactory presenterFactory,
            IPlayerInfo playerInfo)
        {
            _view = view;
            _presenterFactory = presenterFactory;
            _playerInfo = playerInfo;
        }

        [Button]
        public void ShowPopup()
        {
            PlayerPopupPresenter playerPopupPresenter = _presenterFactory.CreatePlayerPopupPresenter();
            _view.Show(playerPopupPresenter);
        }

        [Button]
        public void AddExp(int exp) => _playerInfo.AddExperience(exp);
    }
}