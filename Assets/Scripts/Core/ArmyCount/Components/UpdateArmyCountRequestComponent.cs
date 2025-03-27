using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.ArmyCount.Components
{
    [GameState, FlagPrefix("has")]
    public class UpdateArmyCountRequestComponent : IComponent { }
}