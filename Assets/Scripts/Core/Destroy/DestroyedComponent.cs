using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Destroy
{
    [Game]
    [GameState]
    [Ui]
    [Event(EventTarget.Self)]
    [Cleanup(CleanupMode.DestroyEntity)]
    public sealed class DestroyedComponent : IComponent { }
}