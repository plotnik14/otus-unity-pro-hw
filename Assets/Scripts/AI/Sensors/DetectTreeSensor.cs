using JetBrains.Annotations;
using MBT;
using Trees;
using UnityEngine;

namespace AI.Sensors
{
    public class DetectTreeSensor : MonoBehaviour
    {
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private TreeGroup _treeGroup;

        private BoolVariable _hasAvailableTree;

        [UsedImplicitly]
        private void Awake()
        {
            _hasAvailableTree = _blackboard.GetVariable<BoolVariable>(BlackboardVariableNames.HAS_AVAILABLE_TREE);
            _hasAvailableTree.Value = _treeGroup.CurrentTreesCount > 0;
            _treeGroup.OnTreesCountChanged += OnTreesCountChanged;
        }

        [UsedImplicitly]
        private void OnDestroy() => _treeGroup.OnTreesCountChanged -= OnTreesCountChanged;

        private void OnTreesCountChanged(int currentTreesCount) => _hasAvailableTree.Value = currentTreesCount > 0;
    }
}