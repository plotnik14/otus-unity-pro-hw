using Configs;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Components
{
    [Game, Event(EventTarget.Self)]
    public class TeamComponent : IComponent
    {
        public ETeam value;
    }
}