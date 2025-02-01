using Zenject;

namespace SampleGame
{
    public sealed class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ObjectsFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}