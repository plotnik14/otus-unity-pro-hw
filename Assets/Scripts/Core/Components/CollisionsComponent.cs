using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Components
{
    [Game, Event(EventTarget.Self)]
    public class CollisionsComponent : IComponent
    {
        public List<GameEntity> list;
    }
}