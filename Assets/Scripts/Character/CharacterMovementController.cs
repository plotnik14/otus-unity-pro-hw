using System;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMovementController : IDisposable, INonLazy
    {
        private readonly IInputSystem _input;
        private readonly MoveComponent _characterMovement;

        public CharacterMovementController(IInputSystem input, MoveComponent characterMovement)
        {
            _input = input;
            _characterMovement = characterMovement;

            Subscribe();
        }

        public void Dispose() => Unsubscribe();

        private void Subscribe()
        {
            _input.OnMoveLeft += OnMoveLeft;
            _input.OnMoveRight += OnMoveRight;
            _input.OnStopMovement += OnStopMovement;
        }

        private void Unsubscribe()
        {
            _input.OnMoveLeft -= OnMoveLeft;
            _input.OnMoveRight -= OnMoveRight;
            _input.OnStopMovement -= OnStopMovement;
        }

        private void OnMoveLeft() => _characterMovement.SetDirection(Vector2.left);

        private void OnMoveRight() => _characterMovement.SetDirection(Vector2.right);

        private void OnStopMovement() => _characterMovement.Stop();
    }
}