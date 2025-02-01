using UnityEngine;
using Zenject;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AssetsLoader>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ApplicationStarter>().AsSingle().NonLazy();
            Container.Bind<ApplicationExiter>().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle().NonLazy();
        }
    }
}