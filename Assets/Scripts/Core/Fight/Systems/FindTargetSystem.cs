using System.Collections.Generic;
using Core.Configs;
using Entitas;
using UnityEngine;

namespace Core.Fight.Systems
{
    public class FindTargetSystem : IExecuteSystem
    {
        private readonly UnitConfig _unitConfig;
        private readonly IGroup<GameEntity> _lookingForTargetGroup;
        private readonly IGroup<GameEntity> _possibleTargetGroup;
        private readonly List<GameEntity> _unitBuffer = new();
        private readonly List<GameEntity> _targetBuffer = new();

        public FindTargetSystem(GameContext gameContext, UnitConfig unitConfig)
        {
            _unitConfig = unitConfig;

            _lookingForTargetGroup = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Unit, GameMatcher.Team, GameMatcher.Position)
                .NoneOf(GameMatcher.Target, GameMatcher.Destroyed)
            );

            _possibleTargetGroup = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Unit, GameMatcher.Health, GameMatcher.Team)
                .NoneOf(GameMatcher.Destroyed));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _lookingForTargetGroup.GetEntities(_unitBuffer))
            {
                TrySetTarget(entity);
            }
        }

        private void TrySetTarget(GameEntity entity)
        {
            GameEntity closestEnemy = FindClosestEnemy(entity);

            if (closestEnemy != null)
                entity.AddTarget(closestEnemy);
        }

        private GameEntity FindClosestEnemy(GameEntity attackingEntity)
        {
            ETeam ownTeam = attackingEntity.team.value;
            float attackDistanceSqr = _unitConfig.AttackDistance * _unitConfig.AttackDistance;
            Vector3 attackingEntityPosition = attackingEntity.position.value;
            float minDistanceToEnemySqr = float.MaxValue;
            GameEntity closestEnemy = null;

            foreach (GameEntity possibleEnemy in _possibleTargetGroup.GetEntities(_targetBuffer))
            {
                if (possibleEnemy.team.value == ownTeam)
                    continue;

                Vector3 enemyPosition = possibleEnemy.position.value;
                float distanceToEnemySqr = (enemyPosition - attackingEntityPosition).sqrMagnitude;

                if (distanceToEnemySqr > attackDistanceSqr)
                    continue;

                if (distanceToEnemySqr > minDistanceToEnemySqr)
                    continue;

                minDistanceToEnemySqr = distanceToEnemySqr;
                closestEnemy = possibleEnemy;
            }

            return closestEnemy;
        }
    }
}