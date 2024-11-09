using UnityEngine;

namespace Player
{
    public interface IPlayerInfoChangeHelper
    {
        void AddExperience(int experience);
        void SetName(string name);
        void SetDescription(string description);
        void SetIcon(Sprite icon);
    }
}