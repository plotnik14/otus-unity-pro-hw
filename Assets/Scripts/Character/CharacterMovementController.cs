using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private InputSystem _input;
        [SerializeField] private MoveComponent _characterMovement;

        [UsedImplicitly]
        private void Start() => Subscribe();

        [UsedImplicitly]
        private void OnDestroy() => Unsubscribe();

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