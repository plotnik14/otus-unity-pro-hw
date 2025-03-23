using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Components
{
    [Game, FlagPrefix("has")]
    public class DieRequestComponent : IComponent { }
}