using Entitas;

namespace Core.Views.Components
{
    [Game, GameState, Ui]
    public class ViewComponent : IComponent
    {
        public IEntityView value;
    }
}