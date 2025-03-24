using GameState.Systems;
using Infrastructure;

namespace GameState
{
    public class GameStateSystems : ExtendedFeature
    {
        public GameStateSystems(SystemProvider provider) : base(provider)
        {
            Add<InitArmyCountersSystem>();
            Add<UpdateArmyCountSystem>();
            Add<ArmyCountEventSystem>(); // Generated
        }
    }
}