using Entitas;

namespace Core.Views.Systems
{
    public class ArmyStatisticsUiInitSystem : IInitializeSystem
    {
        private const string ASSET_NAME = "ArmyStatistics";

        private readonly UiContext _uiContext;

        public ArmyStatisticsUiInitSystem(UiContext uiContext) => _uiContext = uiContext;

        public void Initialize()
        {
            UiEntity entity = _uiContext.CreateEntity();
            entity.AddAsset(ASSET_NAME);
        }
    }
}