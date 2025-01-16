using JetBrains.Annotations;
using MBT;
using Units;
using UnityEngine;

namespace AI.Nodes.Actions
{

    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Move To")]
    public class MoveToAction : Leaf
    {
        public GameObjectReference workerObjectReference = new(VarRefMode.DisableConstant);
        public Vector3Reference moveDestinationPointReference = new(VarRefMode.DisableConstant);

        private Worker _worker;

        [UsedImplicitly]
        private void Awake() => _worker = workerObjectReference.Value.GetComponent<Worker>();

        public override NodeResult Execute()
        {
            _worker.MoveTo(moveDestinationPointReference.Value);
            return NodeResult.success;
        }
    }

}