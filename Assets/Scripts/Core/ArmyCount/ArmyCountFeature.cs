using Core.ArmyCount.Systems;
using Infrastructure;

namespace Core.ArmyCount
{
    public class ArmyCountFeature : ExtendedFeature
    {
        public ArmyCountFeature(SystemProvider provider) : base(provider)
        {
            Add<ArmyCountersInitSystem>();
            Add<UpdateArmyCountRxSystem>();
            Add<ArmyCountEventSystem>();
        }
    }
}