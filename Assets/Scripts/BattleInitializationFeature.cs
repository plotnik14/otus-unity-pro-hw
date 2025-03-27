using Core.Systems;
using Infrastructure;

namespace DefaultNamespace
{
    public class BattleInitializationFeature : ExtendedFeature
    {
        public BattleInitializationFeature(SystemProvider provider) : base(provider)
        {
            Add<SpawnArmySystem>();
        }
    }
}