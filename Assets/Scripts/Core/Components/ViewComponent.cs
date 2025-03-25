using Entitas;
using View;

namespace Core.Components
{
    [Game, GameState, Ui]
    public class ViewComponent : IComponent
    {
        public IEntityView value;
    }
}