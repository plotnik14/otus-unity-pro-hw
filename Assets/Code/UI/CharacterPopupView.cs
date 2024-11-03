using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterPopupView : View
    {
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private CharacterInfoView _characterInfoView;
        [SerializeField] private CharacterStatsGroupView _characterStatsGroupView;
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private TMP_Text _levelUpButtonLabel;
        [SerializeField] private Button _closeButton;
    }
}