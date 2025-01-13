using AI.Brains;
using JetBrains.Annotations;
using MBT;
using Trees;
using UnityEngine;
using Tree = Trees.Tree;

namespace AI.Nodes.Actions
{
    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Chop Tree")]
    public class ChopTreeAction : Leaf
    {
        public GameObjectReference workerBrainObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference treeGroupObjectReference = new(VarRefMode.DisableConstant);

        private WorkerBrain _workerBrain;
        private TreeGroup _treeGroup;
        private Tree _nearestTree;

        [UsedImplicitly]
        private void Awake()
        {
            _workerBrain = workerBrainObjectReference.Value.GetComponent<WorkerBrain>();
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

            if (_workerBrain.TryChopTree(_nearestTree))
            {
                // ToDo написать нормальную логику рубки дерева
                _treeGroup.ChopTree(_nearestTree);
                return NodeResult.success;
            }

            return NodeResult.running;
        }

        private void OnTreesCountChanged(int _) => _nearestTree = null;
    }
}