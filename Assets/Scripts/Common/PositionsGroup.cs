using UnityEngine;

namespace ShootEmUp
{
    public class PositionsGroup : MonoBehaviour
    {
        [SerializeField] private Transform[] _positions;

        private int _index = 0;

        public Vector2 GetRandom()
        {
            var randomIndex = Random.Range(0, _positions.Length);
            return _positions[randomIndex].position;
        }

        public Vector2 GetNext()
        {
            Vector2 position = _positions[_index].position;
            _index = ++_index % _positions.Length;
            return position;
        }
    }
}