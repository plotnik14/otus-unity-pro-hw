using Entitas;
using Zenject;

namespace Infrastructure
{
    public class SystemProvider
    {
        private readonly DiContainer _diContainer;

        public SystemProvider(DiContainer diContainer) => _diContainer = diContainer;

        public T Get<T>() where T : ISystem => _diContainer.Resolve<T>();
    }
}