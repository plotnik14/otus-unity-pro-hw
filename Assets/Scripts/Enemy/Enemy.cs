using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour, IGameUpdateListener, IReusable<Enemy>
    {
        public event Action<Enemy> OnDeath;
        public event Action<Enemy> OnInstanceReleased;

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

        public void OnGameUpdate(float deltaTime) => _moveAgent.OnGameUpdate(deltaTime);

        private void OnDestinationReached(EnemyMoveAgent _)
        {
            _moveAgent.OnDestinationReached -= OnDestinationReached;
            _moveAgent.StopMovement();
            _attackAgent.StartAttackingTarget(_attackTarget);
        }

        private void OnDeathHandler(HitPointsComponent _)
        {
            _moveAgent.StopMovement();
            _attackAgent.StopAttackingTarget();
            _enemyHpComponent.OnDeath -= OnDeathHandler;
            _moveAgent.OnDestinationReached -= OnDestinationReached;
            OnDeath.SafeInvoke(this);
            OnInstanceReleased.SafeInvoke(this);
        }
    }
}