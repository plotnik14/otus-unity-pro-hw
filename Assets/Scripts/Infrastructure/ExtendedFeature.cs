using Entitas;

namespace Infrastructure
{
    public class ExtendedFeature : Feature
    {
        private readonly SystemProvider _provider;

        public ExtendedFeature(string name, SystemProvider provider) : base(name) => _provider = provider;

        public ExtendedFeature(SystemProvider provider) => _provider = provider;

        protected void Add<T>() where T : ISystem
        {
            T system = _provider.Get<T>();
            Add(system);
        }
    }
}