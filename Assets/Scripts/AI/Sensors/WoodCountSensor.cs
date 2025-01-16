using JetBrains.Annotations;
using MBT;
using Units;
using UnityEngine;

namespace AI.Sensors
{
    public class WoodCountSensor : MonoBehaviour
    {
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private Inventory _inventory;

        private IntVariable _collectedWoodCount;

        [UsedImplicitly]
        private void Awake()
        {
            _collectedWoodCount = _blackboard.GetVariable<IntVariable>(BlackboardVariableNames.COLLECTED_WOOD);
            _collectedWoodCount.Value = _inventory.WoodCount;
            _inventory.OnWoodCountChanged += OnWoodCountChanged;
        }

        [UsedImplicitly]
        private void OnDestroy() => _inventory.OnWoodCountChanged -= OnWoodCountChanged;

        private void OnWoodCountChanged(int currentWoodCount) => _collectedWoodCount.Value = currentWoodCount;
    }

}