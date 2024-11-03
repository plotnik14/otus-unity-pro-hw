using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterAvatarView : View
    {
        [SerializeField] private Image _avatar;

        public void SetAvatar(Image avatar) => _avatar = avatar;
    }
}