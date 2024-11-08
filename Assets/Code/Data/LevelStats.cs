using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    // [CreateAssetMenu(fileName = "LevelStats", menuName = "Data/LevelStats")]
    [Serializable]
    public class LevelStats// : ScriptableObject
    {
        [SerializeField] private int _level;
        [SerializeField] private List<Stat> _stats;

        public int Level => _level;
        public List<Stat> Stats => _stats;
    }
}