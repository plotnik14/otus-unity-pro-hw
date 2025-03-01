using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Core.Components
{
    [Game, Event(EventTarget.Self)] // ToDO удалить ивент
    public class CollisionsComponent : IComponent
    {
        public List<GameEntity> list;
    }
}