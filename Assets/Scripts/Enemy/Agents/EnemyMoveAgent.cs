using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class EnemyMoveAgent : MonoBehaviour, IGameUpdateListener
    {
        public Action<EnemyMoveAgent> OnDestinationReached;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private float _reachDestinationRadius = 0.25f;

        private Vector2 _destination;

        public bool IsDestinationReached { get; private set; }

        public void OnGameUpdate(float _)
        {
            if (IsDestinationReached)
            {
                return;
            }

            float distanceToDestination = Vector2.Distance(_destination, transform.position);

            if (distanceToDestination <= _reachDestinationRadius)
            {
                IsDestinationReached = true;
                OnDestinationReached.SafeInvoke(this);
            }
        }

        public void StartMovementToDestination(Vector2 destination)
        {
            _destination = destination;
            IsDestinationReached = false;
            Vector2 direction = (_destination - (Vector2)transform.position).normalized;
            _moveComponent.SetDirection(direction);
        }

        public void StopMovement() => _moveComponent.Stop();
    }
}