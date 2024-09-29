using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class InputSystem : MonoBehaviour
    {
        public event Action OnFire;
        public event Action OnMoveLeft;
        public event Action OnMoveRight;
        public event Action OnStopMovement;

        [UsedImplicitly]
        private void Update()
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