using TMPro;
using UnityEngine;

namespace UI.PlayerPopup.View
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statInfo;

        public void SetInfo(string statName, int statValue) => _statInfo.text = $"{statName}: {statValue}";
    }
}