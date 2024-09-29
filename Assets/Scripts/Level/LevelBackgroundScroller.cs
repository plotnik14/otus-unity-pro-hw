using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class LevelBackgroundScroller : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _scrollSpeedY;

        private float positionX;
        private float positionZ;

        private bool NeedToResetPosition => transform.position.y <= _endPositionY;

        [UsedImplicitly]
        private void Awake()
        {
            Vector3 position = transform.position;
            positionX = position.x;
            positionZ = position.z;
            _gameManager.RegisterListener(this);
        }

        public void OnGameUpdate(float deltaTime) => ScrollBackground(deltaTime);

        private void ScrollBackground(float deltaTime)
        {
            if (NeedToResetPosition)
            {
                UpdatePosition(_startPositionY);
                return;
            }

            float newPositionY = transform.position.y - _scrollSpeedY * deltaTime;
            UpdatePosition(newPositionY);
        }

        private void UpdatePosition(float positionY) => transform.position = new Vector3(positionX, positionY, positionZ);
    }
}