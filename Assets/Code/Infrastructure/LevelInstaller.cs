using Data;
using Player;
using UI.PlayerPopup.Factory;
using UI.PlayerPopup.View;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerLevelsConfiguration _playerLevelsConfiguration;
        [SerializeField] private PlayerPopupView _playerPopupView;

        [Space] [Header("Player Info on Start")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _currentExperience;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerLevelsConfiguration);
            Container.BindInstance(_playerPopupView);
            Container.BindInterfacesTo<PlayerInfo>()
                .AsSingle()
                .WithArguments(_name, _description, _icon, _currentLevel, _currentExperience);
            Container.Bind<PlayerPopupPresenterFactory>().AsSingle().NonLazy();
        }
    }
}