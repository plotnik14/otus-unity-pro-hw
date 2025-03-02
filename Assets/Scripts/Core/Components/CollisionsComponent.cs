using System.Collections.Generic;
using Entitas;

namespace Core.Components
{
    [Game]
    public class CollisionsComponent : IComponent
    {
        public List<GameEntity> list;
    }
}