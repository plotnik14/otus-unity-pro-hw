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
        [SerializeField] private Sprite _avatar1;
        [SerializeField] private Sprite _avatar2;
        [SerializeField] private Sprite _avatar3;

        private PlayerPopupView _view;
        private PlayerPopupPresenterFactory _presenterFactory;
        private IPlayerInfoChangeHelper _playerInfo;

        [Inject]
        public void Construct(
            PlayerPopupView view,
            PlayerPopupPresenterFactory presenterFactory,
            IPlayerInfoChangeHelper playerInfo)
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

        [Button]
        public void SetName(string newName) => _playerInfo.SetName(newName);

        [Button]
        public void SetDescription(string newDescription) => _playerInfo.SetDescription(newDescription);

        [Button]
        public void SetAvatar1() => _playerInfo.SetIcon(_avatar1);

        [Button]
        public void SetAvatar2() => _playerInfo.SetIcon(_avatar2);

        [Button]
        public void SetAvatar3() => _playerInfo.SetIcon(_avatar3);
    }
}