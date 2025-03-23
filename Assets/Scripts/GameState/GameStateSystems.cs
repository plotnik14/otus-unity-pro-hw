using System.Collections.Generic;
using GameState.Systems;
using UI;

namespace GameState
{
    public class GameStateSystems : Feature
    {
        public GameStateSystems(Contexts contexts, List<UiArmyCountView> uiArmyCountViews)
        {
            Add(new InitArmyCountersSystem(contexts.gameState, uiArmyCountViews));
            Add(new UpdateArmyCountSystem(contexts.gameState, contexts.game));

            Add(new ArmyCountEventSystem(contexts)); // Generated
        }
    }
}