using System.Collections.Generic;
using Data;
using UniRx;
using UnityEngine;

namespace Player
{
    public interface IPlayerInfo
    {
        IReadOnlyReactiveProperty<string> Name { get; }
        IReadOnlyReactiveProperty<string> Description { get; }
        IReadOnlyReactiveProperty<Sprite> Icon { get; }
        IReadOnlyReactiveProperty<int> CurrentLevel { get; }
        IReadOnlyReactiveProperty<int> CurrentExperience { get; }
        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        IReadOnlyList<Stat> StatsForCurrentLevel { get; }
        int RequiredExpForLevelUp { get; }

        void AddExperience(int experience);
        void LevelUp();
    }
}