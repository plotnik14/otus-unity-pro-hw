using Entitas;
using View;

namespace Core.Components
{
    [Game, GameState]
    public class ViewComponent : IComponent
    {
        public IEntityView value;
    }
}