using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Components
{
    [Event(EventTarget.Self), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class DestroyedComponent : IComponent { }
}