using Core.Movement.Systems;
using Infrastructure;

namespace Core.Movement
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