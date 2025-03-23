using Configs;
using Entitas;

namespace GameState.Components
{
    [GameState]
    public class StateTeamComponent : IComponent
    {
        public ETeam value;
    }
}