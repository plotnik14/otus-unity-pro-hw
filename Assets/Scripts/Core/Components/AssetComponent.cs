using Entitas;

namespace Core.Components
{
    [Game, GameState, Ui]
    public class AssetComponent : IComponent
    {
        public string value;
    }
}