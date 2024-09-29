using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _damage;

        [UsedImplicitly]
        private void OnCollisionEnter2D(Collision2D collision)
        {
            ProcessCollision(collision);
            OnCollisionEntered.SafeInvoke(this, collision);
        }

        public void Initialize(BulletConfig config)
        {
            gameObject.layer = (int)config.PhysicsLayer;
            _spriteRenderer.color = config.Color;
            _moveComponent.SetSpeed(config.Speed);
            _damage = config.Damage;
        }

        public void Launch(Vector2 startPosition, Vector2 direction)
        {
            transform.position = startPosition;
            _moveComponent.SetDirection(direction);

        }

        private void ProcessCollision(Collision2D collision2D)
        {
            GameObject collidedObject = collision2D.gameObject;

            if (collidedObject.TryGetComponent(out HitPointsComponent hitPointsComponent))
            {
                hitPointsComponent.TakeDamage(_damage);
            }
        }
    }
}