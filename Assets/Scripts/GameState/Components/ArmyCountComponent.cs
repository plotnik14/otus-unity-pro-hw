using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace GameState.Components
{
    [GameState, Event(EventTarget.Self)]
    public class ArmyCountComponent : IComponent
    {
        public int value;
    }
}