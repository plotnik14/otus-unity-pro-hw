using Core.Views.Systems;
using Infrastructure;

namespace Core.Views
{
    public class ViewProcessingFeature : ExtendedFeature
    {
        public ViewProcessingFeature(SystemProvider provider) : base(provider)
        {
            Add<ArmyStatisticsUiInitSystem>();
            Add<CreateViewMultiRxSystem>();
            Add<TeamEventSystem>();
        }
    }
}