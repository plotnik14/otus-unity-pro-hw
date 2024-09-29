using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class EnemyAI : MonoBehaviour
    {
        public event Action<EnemyAI> OnDeath;

        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private EnemyAttackAgent _attackAgent;
        [SerializeField] private HitPointsComponent _enemyHpComponent;

        private Vector2 _attackPosition;
        private GameObject _attackTarget;

        public void Initialize(Vector2 attackPosition, GameObject attackTarget)
        {
            _attackPosition = attackPosition;
            _attackTarget = attackTarget;
        }

        public void Activate()
        {
            _enemyHpComponent.OnDeath += OnDeathHandler;
            _moveAgent.OnDestinationReached += OnDestinationReached;
            _moveAgent.StartMovementToDestination(_attackPosition);
        }

        private void OnDestinationReached(EnemyMoveAgent _)
        {
            _moveAgent.OnDestinationReached -= OnDestinationReached;
            _moveAgent.StopMovement();
            _attackAgent.StartAttackingTarget(_attackTarget);
        }

        private void OnDeathHandler(HitPointsComponent _)
        {
            _attackAgent.StopAttackingTarget();
            _enemyHpComponent.OnDeath -= OnDeathHandler;
            _moveAgent.OnDestinationReached -= OnDestinationReached;
            OnDeath.SafeInvoke(this);
        }
    }
}