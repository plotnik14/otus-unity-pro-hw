using Entitas;

namespace Core.Components
{
    [Game, GameState]
    public class AssetComponent : IComponent
    {
        public string value;
    }
}