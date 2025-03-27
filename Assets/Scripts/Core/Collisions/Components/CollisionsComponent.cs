using System.Collections.Generic;
using Entitas;

namespace Core.Collisions.Components
{
    [Game]
    public class CollisionsComponent : IComponent
    {
        public List<GameEntity> list;
    }
}