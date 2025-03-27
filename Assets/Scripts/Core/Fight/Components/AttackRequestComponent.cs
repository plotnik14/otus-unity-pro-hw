using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Fight.Components
{
    [Game, FlagPrefix("has")]
    public class AttackRequestComponent : IComponent { }
}