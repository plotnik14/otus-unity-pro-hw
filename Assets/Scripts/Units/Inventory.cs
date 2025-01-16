using System;
using UnityEngine;
using Utils;

namespace Units
{
    public class Inventory : MonoBehaviour
    {
        public event Action<int> OnWoodCountChanged;

        private int _woodCount;

        public int WoodCount
        {
            get => _woodCount;
            set
            {
                _woodCount = value;
                OnWoodCountChanged.SafeInvoke(_woodCount);
            }
        }
    }
}