using Core.Configs;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Views.Components
{
    [Game, Event(EventTarget.Self)]
    public class TeamComponent : IComponent
    {
        public ETeam value;
    }
}