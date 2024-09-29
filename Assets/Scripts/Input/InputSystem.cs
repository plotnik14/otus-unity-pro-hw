using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class InputSystem : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private GameManager _gameManager;

        public event Action OnFire;
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnStopMovement;

        [UsedImplicitly]
        private void Awake() => _gameManager.RegisterListener(this);

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