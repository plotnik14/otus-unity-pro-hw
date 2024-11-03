using TMPro;
using UnityEngine;

namespace UI
{
    public class CharacterInfoView : View
    {
        [SerializeField] private CharacterAvatarView _avatarView;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private CharacterXpBarView _xpBarView;
    }
}