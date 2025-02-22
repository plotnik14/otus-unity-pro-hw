using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class FindTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _lookingForTargetGroup;
        private readonly IGroup<GameEntity> _possibleTargetGroup;

        public FindTargetSystem(GameContext gameContext)
        {
            _lookingForTargetGroup = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Attack, GameMatcher.Team, GameMatcher.Position)
                .NoneOf(GameMatcher.Target)
            );

            _possibleTargetGroup = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Health, GameMatcher.Team));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _lookingForTargetGroup.GetEntities())
            {
                TrySetTarget(entity);
            }
        }

        private void TrySetTarget(GameEntity entity)
        {
            GameEntity closestEnemy = FindClosestEnemy(entity);

            // ToDo через Request?

            if (closestEnemy != null)
                entity.AddTarget(closestEnemy);
        }

        private GameEntity FindClosestEnemy(GameEntity attackingEntity)
        {
            ETeam ownTeam = attackingEntity.team.value;
            float attackDistance = attackingEntity.attack.distance;
            Vector3 attackingEntityPosition = attackingEntity.position.value;
            float minDistanceToEnemy = float.MaxValue;
            GameEntity closestEnemy = null;

            foreach (GameEntity possibleEnemy in _possibleTargetGroup.GetEntities())
            {
                if (possibleEnemy.team.value == ownTeam)
                    continue;

                Vector3 enemyPosition = possibleEnemy.position.value;
                float distanceToEnemy = (enemyPosition - attackingEntityPosition).magnitude; // ToDO optimize with Sqr?

                if (distanceToEnemy > attackDistance)
                    continue;

                if (distanceToEnemy > minDistanceToEnemy)
                    continue;

                minDistanceToEnemy = distanceToEnemy;
                closestEnemy = possibleEnemy;
            }

            return closestEnemy;
        }
    }
}