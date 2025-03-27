using Core.Collisions.Systems;
using Infrastructure;

namespace Core.Collisions
{
    public class CollisionProcessingFeature : ExtendedFeature
    {
        public CollisionProcessingFeature(SystemProvider provider) : base(provider)
        {
            Add<UnitCollisionRxSystem>();
            Add<ProjectileCollisionRxSystem>();
        }
    }
}