using Core.Systems;
using Infrastructure;

namespace View
{
    public class MovementFeature : ExtendedFeature
    {
        public MovementFeature(SystemProvider provider) : base(provider)
        {
            Add<MovementSystem>();
            Add<RotationSystem>();

            Add<PositionEventSystem>();
            Add<RotationEventSystem>();
        }
    }
}