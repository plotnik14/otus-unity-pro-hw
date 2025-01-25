using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class MoveController : IFixedTickable
    {
        private readonly ICharacter _character;
        private readonly IMoveInput _moveInput;

        public MoveController(ICharacter character, IMoveInput moveInput)
        {
            _character = character;
            _moveInput = moveInput;
        }

        void IFixedTickable.FixedTick() => _character.Move(_moveInput.GetDirection(), Time.deltaTime);
    }
}