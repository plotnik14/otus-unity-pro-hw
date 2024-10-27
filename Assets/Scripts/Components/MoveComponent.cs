using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public class MoveComponent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed = 5.0f;

        private IGameManager _gameManager;
        private Vector2 _direction;

        [Inject]
        public void Construct(IGameManager gameManager) => _gameManager = gameManager;

        [UsedImplicitly]
        private void Awake() => _gameManager.RegisterListener(this);

        [UsedImplicitly]
        private void OnDestroy() => _gameManager.UnregisterListener(this);

        public void OnGameFixedUpdate(float deltaTime) => ProcessMovement(deltaTime);

        public void SetDirection(Vector2 direction) => _direction = direction;

        public void SetSpeed(float speed) => _speed = speed;

        public void Stop() => _direction = Vector2.zero;

        private void ProcessMovement(float deltaTime)
        {
            if (_direction == Vector2.zero)
            {
                return;
            }

            Vector2 nextPosition = GetNextPosition(deltaTime);
            _rigidbody2D.MovePosition(nextPosition);
        }

        private Vector2 GetNextPosition(float deltaTime) => _rigidbody2D.position + _direction * (_speed * deltaTime);
    }
}