using Core.Configs;
using TMPro;
using UnityEngine;

namespace Core.ArmyCount.Views
{
    public class UiArmyCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;
        [SerializeField] private ETeam _team;

        public ETeam Team => _team;

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);

        public void SetCount(int count) => _count.text = count.ToString();
    }
}