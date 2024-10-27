using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class InputSystem : IInputSystem, IGameUpdateListener, IDisposable, INonLazy
    {
        public event Action OnFire;
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnStopMovement;

        private readonly IGameManager _gameManager;

        public InputSystem(IGameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.RegisterListener(this);
        }

        public void Dispose() => _gameManager.UnregisterListener(this);

        public void OnGameUpdate(float _)
        {
            ProcessMovementInput();
            ProcessFireInput();
        }

        private void ProcessFireInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFire.SafeInvoke();
            }
        }

        private void ProcessMovementInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnMoveLeft.SafeInvoke();
                return;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                OnMoveRight.SafeInvoke();
                return;
            }

            OnStopMovement.SafeInvoke();
        }
    }
}