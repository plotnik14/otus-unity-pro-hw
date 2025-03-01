using Engine.Configs;
using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class SpawnArmySystem : IInitializeSystem
    {
        private readonly GameContext _gameContext;
        private readonly UnitConfig _unitConfig;
        private readonly SpawnArmyConfig _spawnArmyConfig;

        public SpawnArmySystem(
            GameContext gameContext,
            UnitConfig unitConfig,
            SpawnArmyConfig spawnArmyConfig)
        {
            _gameContext = gameContext;
            _unitConfig = unitConfig;
            _spawnArmyConfig = spawnArmyConfig;
        }

        public void Initialize()
        {
            SpawnArmy(ETeam.BlueTeam, _spawnArmyConfig.BlueTeamCount, _spawnArmyConfig.BlueTeamStartSpawnPosition, _spawnArmyConfig.BlueTeamStartDirection);
            SpawnArmy(ETeam.RedTeam,  _spawnArmyConfig.RedTeamCount,  _spawnArmyConfig.RedTeamStartSpawnPosition,  _spawnArmyConfig.RedTeamStartDirection);
        }

        private void SpawnArmy(ETeam team, int count, Vector3 startPosition, Vector3 direction)
        {
            Vector3 nextSpawnPosition = startPosition;

            for (int index = 0; index < count; index++)
            {
                CreateUnit(team, direction, nextSpawnPosition);
                nextSpawnPosition = GetNextPosition(nextSpawnPosition);
            }
        }

        private void CreateUnit(ETeam team, Vector3 direction, Vector3 position)
        {
            GameEntity entity = _gameContext.CreateEntity();
            entity.AddTeam(team);
            entity.AddPosition(position);
            entity.AddDirection(direction);
            entity.AddRotation(direction);
            entity.AddAsset(_unitConfig.AssetName);
            entity.AddMovementSpeed(_unitConfig.MovementSpeed);
            entity.AddRotationSpeed(_unitConfig.RotationSpeed);
            entity.AddAttack(_unitConfig.AttackDistance);
            entity.AddHealth(_unitConfig.Health);
            entity.isUnit = true;
        }

        private Vector3 GetNextPosition(Vector3 position)
        {
            position.x += _spawnArmyConfig.SpawnOffset;
            position.x += Random.Range(-1, 1);
            position.z += Random.Range(-1, 1);
            return position;
        }
    }
}