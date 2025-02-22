using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class SpawnArmySystem : IInitializeSystem
    {
        // ToDo вынести в конфигурацию??
        private const int BLUE_TEAM_COUNT = 3;
        private const int RED_TEAM_COUNT = 3;
        private const float SPAWN_OFFSET = 1.3f;
        private const float MOVEMENT_SPEED = 0.7f;
        private const float ROTATION_SPEED = 0.7f;
        private const string RANGE_UNIT_ASSET_NAME = "RangeUnit";
        private readonly Vector3 BLUE_TEAM_START_POSITION = new(10, 0, -10);
        private readonly Vector3 RED_TEAM_START_POSITION = new(10, 0, 10);
        private readonly Vector3 BLUE_TEAM_DIRECTION = Vector3.forward;
        private readonly Vector3 RED_TEAM_DIRECTION = Vector3.back;

        private readonly GameContext _gameContext;

        public SpawnArmySystem(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Initialize()
        {
            SpawnArmy(ETeam.BlueTeam, BLUE_TEAM_COUNT, BLUE_TEAM_START_POSITION, BLUE_TEAM_DIRECTION);
            SpawnArmy(ETeam.RedTeam, RED_TEAM_COUNT, RED_TEAM_START_POSITION, RED_TEAM_DIRECTION);
        }

        private void SpawnArmy(ETeam team, int count, Vector3 startPosition, Vector3 direction)
        {
            Vector3 nextSpawnPosition = startPosition;

            for (int index = 0; index < count; index++)
            {
                GameEntity entity = _gameContext.CreateEntity();
                entity.AddTeam(team);
                entity.AddPosition(nextSpawnPosition);
                entity.AddDirection(direction);
                entity.AddRotation(direction);
                entity.AddAsset(RANGE_UNIT_ASSET_NAME);
                entity.AddMovementSpeed(MOVEMENT_SPEED);
                entity.AddRotationSpeed(ROTATION_SPEED);
                nextSpawnPosition.x += SPAWN_OFFSET;
            }
        }
    }
}