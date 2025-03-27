using GameState.Systems;
using Infrastructure;

namespace DefaultNamespace
{
    public class ArmyCountFeature : ExtendedFeature
    {
        public ArmyCountFeature(SystemProvider provider) : base(provider)
        {
            Add<InitArmyCountersSystem>();
            Add<UpdateArmyCountSystem>();
            Add<ArmyCountEventSystem>();
        }
    }
}