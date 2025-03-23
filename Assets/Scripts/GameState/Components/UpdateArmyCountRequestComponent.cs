using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace GameState.Components
{
    [GameState, FlagPrefix("has")]
    public class UpdateArmyCountRequestComponent : IComponent { }
}