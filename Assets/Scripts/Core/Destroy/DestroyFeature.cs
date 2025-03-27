using Infrastructure;

namespace Core.Destroy
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