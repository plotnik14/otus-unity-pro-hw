using Core.Systems;
using Infrastructure;
using UI;

namespace View
{
    public class ViewProcessingFeature : ExtendedFeature
    {
        public ViewProcessingFeature(SystemProvider provider) : base(provider)
        {
            Add<InitArmyStatisticsUiSystem>();
            Add<MultiCreateViewSystem>();
            Add<TeamEventSystem>();
        }
    }
}