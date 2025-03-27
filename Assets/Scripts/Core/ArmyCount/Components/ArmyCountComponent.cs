using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.ArmyCount.Components
{
    [GameState, Event(EventTarget.Self)]
    public class ArmyCountComponent : IComponent
    {
        public int value;
    }
}