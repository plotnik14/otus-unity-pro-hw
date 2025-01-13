using UnityEngine;

namespace Trees
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private int _woodCount = 1;

        public int WoodCount => _woodCount;
    }
}