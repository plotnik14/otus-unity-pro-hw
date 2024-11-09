using System.Collections.Generic;
using Data;
using UniRx;
using UnityEngine;

namespace Player
{
    public sealed class PlayerInfo : IPlayerInfo, IPlayerInfoChangeHelper
    {
        private readonly PlayerLevelsConfiguration _levelsConfiguration;

        private readonly ReactiveProperty<string> _name = new();
        private readonly ReactiveProperty<string> _description = new();
        private readonly ReactiveProperty<Sprite> _icon = new();
        private readonly ReactiveProperty<int> _currentLevel = new();
        private readonly ReactiveProperty<int> _currentExperience = new();
        private readonly ReactiveProperty<bool> _canLevelUp = new();

        public IReadOnlyReactiveProperty<string> Name => _name;
        public IReadOnlyReactiveProperty<string> Description => _description;
        public IReadOnlyReactiveProperty<Sprite> Icon => _icon;
        public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        public IReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;
        public IReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        public IReadOnlyList<Stat> StatsForCurrentLevel => _levelsConfiguration.Levels[CurrentLevel.Value - 1].Stats;

        public int RequiredExpForLevelUp => 100 * (_currentLevel.Value + 1);
        private int MaxAvailableLevel => _levelsConfiguration.Levels.Count;

        public PlayerInfo(
            string name,
            string description,
            Sprite icon,
            int currentLevel,
            int currentExperience,
            PlayerLevelsConfiguration levelsConfiguration)
        {
            _name.Value = name;
            _description.Value = description;
            _icon.Value = icon;
            _currentLevel.Value = currentLevel;
            _currentExperience.Value = currentExperience;
            _levelsConfiguration = levelsConfiguration;
        }

        public void LevelUp()
        {
            if (_canLevelUp.Value)
            {
                _currentExperience.Value = 0;
                _currentLevel.Value++;
                _canLevelUp.Value = false;
            }
        }

        public void AddExperience(int experience)
        {
            int newExp = _currentExperience.Value + experience;
            newExp = Mathf.Clamp(newExp, 0, RequiredExpForLevelUp);
            _currentExperience.Value = newExp;
            _canLevelUp.Value = _currentExperience.Value == RequiredExpForLevelUp
                                && _currentLevel.Value < MaxAvailableLevel;
        }

        public void SetName(string name) => _name.Value = name;

        public void SetDescription(string description) => _description.Value = description;

        public void SetIcon(Sprite icon) => _icon.Value = icon;
    }
}