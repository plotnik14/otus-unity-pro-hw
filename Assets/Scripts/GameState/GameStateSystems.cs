using GameState.Systems;

namespace GameState
{
    public class GameStateSystems : Feature
    {
        public GameStateSystems(Contexts contexts)
        {
            Add(new InitArmyCountersSystem(contexts.gameState));
            Add(new UpdateArmyCountSystem(contexts.gameState, contexts.game));

            Add(new ArmyCountEventSystem(contexts)); // Generated
        }
    }
}