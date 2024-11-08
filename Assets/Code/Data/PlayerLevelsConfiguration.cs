using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerLevelsConfiguration", menuName = "Data/PlayerLevelsConfiguration")]
    public class PlayerLevelsConfiguration : ScriptableObject
    {
        [SerializeField] private List<LevelStats> _levels;

        public List<LevelStats> Levels => _levels;
    }
}