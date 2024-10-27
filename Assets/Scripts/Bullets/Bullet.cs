using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class Bullet : MonoBehaviour, IReusable<Bullet>
    {
        public event Action<Bullet> OnInstanceReleased;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _damage;

        [UsedImplicitly]
        private void OnCollisionEnter2D(Collision2D collision) => ProcessCollision(collision);

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

            if (collidedObject.TryGetComponent(out TakeDamageAction action))
            {
                action.TakeDamage(_damage);
            }

            OnInstanceReleased.SafeInvoke(this);
        }
    }
}