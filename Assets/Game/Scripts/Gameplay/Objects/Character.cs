using UnityEngine;

namespace SampleGame
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private float _speed = 2.5f;

        public Vector3 Position => transform.position;

        public void Move(Vector3 direction, float deltaTime) => transform.position += direction * (deltaTime * _speed);
    }
}