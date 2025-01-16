using JetBrains.Annotations;
using MBT;
using Trees;
using Units;
using UnityEngine;
using Tree = Trees.Tree;

namespace AI.Nodes.Actions
{
    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Select Nearest Tree")]
    public class SelectNearestTreeAction : Leaf
    {
        public GameObjectReference workerObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference treeGroupObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference selectedTreeObjectReference = new(VarRefMode.DisableConstant);
        public TransformReference targetTransformReference = new(VarRefMode.DisableConstant);

        private Worker _workerBrain;
        private TreeGroup _treeGroup;
        private Tree _nearestTree;

        [UsedImplicitly]
        private void Awake()
        {
            _workerBrain = workerObjectReference.Value.GetComponent<Worker>();
            _treeGroup = treeGroupObjectReference.Value.GetComponent<TreeGroup>();
            _treeGroup.OnTreesCountChanged += OnTreesCountChanged;
        }

        [UsedImplicitly]
        private void OnDestroy() => _treeGroup.OnTreesCountChanged -= OnTreesCountChanged;

        public override NodeResult Execute()
        {
            if (_nearestTree is null)
                _nearestTree = _treeGroup.GetNearestTree(_workerBrain.transform.position);

            if (_nearestTree is null)
                return NodeResult.failure;

            selectedTreeObjectReference.Value = _nearestTree.gameObject;
            targetTransformReference.Value = _nearestTree.transform;
            return NodeResult.success;
        }

        private void OnTreesCountChanged(int _) => _nearestTree = null;
    }
}