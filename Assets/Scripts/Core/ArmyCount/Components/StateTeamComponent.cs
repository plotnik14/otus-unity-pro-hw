using Core.Configs;
using Entitas;

namespace Core.ArmyCount.Components
{
    [GameState]
    public class StateTeamComponent : IComponent
    {
        public ETeam value;
    }
}