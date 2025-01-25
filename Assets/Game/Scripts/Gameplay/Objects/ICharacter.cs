using UnityEngine;

namespace SampleGame
{
    public interface ICharacter
    {
        Vector3 Position { get; }

        void Move(Vector3 direction, float deltaTime);
    }
}