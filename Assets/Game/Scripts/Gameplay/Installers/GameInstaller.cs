using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private Camera _camera;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private Transform _locationsContainer;

        public override void InstallBindings()
        {
            Container
                .Bind<ObjectsFactory>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<Camera>()
                .FromInstance(_camera);

            Container
                .Bind<ICharacter>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesTo<MoveController>()
                .AsCached()
                .NonLazy();
            
            Container
                .Bind<IMoveInput>()
                .To<MoveInput>()
                .AsSingle()
                .WithArguments(_inputConfig)
                .NonLazy();

            Container
                .BindInterfacesTo<CameraFollower>()
                .AsCached()
                .WithArguments(_cameraConfig.cameraOffset)
                .NonLazy();

            Container
                .Bind<LocationLoader>()
                .AsSingle()
                .WithArguments(_locationsContainer);
        }
    }
}