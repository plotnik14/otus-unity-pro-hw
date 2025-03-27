using Core.Movement.Systems;
using Infrastructure;

namespace Core.Movement
{
    public class MovementFeature : ExtendedFeature
    {
        public MovementFeature(SystemProvider provider) : base(provider)
        {
            Add<MovementExSystem>();
            Add<RotationExSystem>();
            Add<PositionEventSystem>();
            Add<RotationEventSystem>();
        }
    }
}