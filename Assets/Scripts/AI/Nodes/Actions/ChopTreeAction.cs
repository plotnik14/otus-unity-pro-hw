using JetBrains.Annotations;
using MBT;
using Units;
using UnityEngine;
using Tree = Trees.Tree;

namespace AI.Nodes.Actions
{
    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Chop Tree")]
    public class ChopTreeAction : Leaf
    {
        public GameObjectReference workerObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference selectedTreeObjectReference = new(VarRefMode.DisableConstant);

        private Worker _worker;

        [UsedImplicitly]
        private void Awake() => _worker = workerObjectReference.Value.GetComponent<Worker>();

        public override NodeResult Execute()
        {
            if (selectedTreeObjectReference.Value is null)
                return NodeResult.failure;

            Tree selectedTree = selectedTreeObjectReference.Value.GetComponent<Tree>();

            if (_worker.TryChopTree(selectedTree))
            {
                selectedTreeObjectReference.Value = null;
                return NodeResult.success;
            }

            return NodeResult.running;
        }
    }
}