using Infrastructure;

namespace Core.BattleInitialization
{
    public class BattleInitializationFeature : ExtendedFeature
    {
        public BattleInitializationFeature(SystemProvider provider) : base(provider)
        {
            Add<SpawnArmyInitSystem>();
        }
    }
}