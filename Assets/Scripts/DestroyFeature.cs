using Core.Systems;
using Infrastructure;

namespace DefaultNamespace
{
    public class DestroyFeature : ExtendedFeature
    {
        public DestroyFeature(SystemProvider provider) : base(provider)
        {
            Add<GameDestroyedEventSystem>();
            Add<GameStateDestroyedEventSystem>();
            Add<UiDestroyedEventSystem>();
            Add<MultiDestroySystem>();
        }
    }
}