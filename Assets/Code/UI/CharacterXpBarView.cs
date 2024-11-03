using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterXpBarView : View
    {
        [SerializeField] private TMP_Text _progressValueText;
        [SerializeField] private Image _progressValueImage;
        [SerializeField] private Sprite _completedProgressImage;
        [SerializeField] private Sprite _notCompletedProgressImage;


    }
}