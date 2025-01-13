using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Buildings
{
    public class WorkTable : MonoBehaviour
    {
        [SerializeField] private TMP_Text _woodCountText;

        private int _woodCount = 0;

        [UsedImplicitly]
        private void Awake() => UpdateCountWidget();

        public void AddWood(int count)
        {
            _woodCount += count;
            UpdateCountWidget();
        }

        private void UpdateCountWidget() => _woodCountText.text = _woodCount.ToString();
    }
}