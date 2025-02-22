using Entitas;

namespace Core.Components
{
    [Game]
    public class AttackComponent : IComponent
    {
        public float distance; // ToDO вынести в AttackDistanceComponent ?? А текущую оставить CanAttack?
    }
}