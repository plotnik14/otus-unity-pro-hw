using System;
using UnityEngine;
using Utils;

namespace Trees
{
    public class Tree : MonoBehaviour
    {
        public event Action<Tree> OnChopped;

        [SerializeField] private int _woodCount = 1;

        public int WoodCount => _woodCount;

        public void Chop() => OnChopped.SafeInvoke(this);
    }
}