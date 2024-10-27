using UnityEngine;

namespace ShootEmUp
{
    public class EnemyActivator : IEnemyActivator, INonLazy
    {
        private readonly GameObject _attackTarget;
        private readonly PositionsGroup _attackPositions;

        public EnemyActivator(GameObject attackTarget, PositionsGroup attackPositions)
        {
            _attackTarget = attackTarget;
            _attackPositions = attackPositions;
        }

        public void Activate(Enemy enemy)
        {
            Vector2 attackPosition = _attackPositions.GetRandom();
            enemy.Initialize(attackPosition, _attackTarget);
            enemy.Activate();
        }
    }
}