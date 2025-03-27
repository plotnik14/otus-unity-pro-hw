using Core.Systems;
using Infrastructure;

namespace DefaultNamespace
{
    public class CollisionProcessingFeature : ExtendedFeature
    {
        public CollisionProcessingFeature(SystemProvider provider) : base(provider)
        {
            Add<UnitCollisionSystem>();
            Add<ProjectileCollisionSystem>();
        }
    }
}