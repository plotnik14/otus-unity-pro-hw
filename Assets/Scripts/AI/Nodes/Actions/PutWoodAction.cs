using Buildings;
using JetBrains.Annotations;
using MBT;
using Units;
using UnityEngine;

namespace AI.Nodes.Actions
{
    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Put Wood")]
    public class PutWoodAction : Leaf
    {
        public GameObjectReference workerObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference workTableObjectReference = new(VarRefMode.DisableConstant);

        private Worker _worker;
        private WorkTable _workTable;

        [UsedImplicitly]
        private void Awake()
        {
            _worker = workerObjectReference.Value.GetComponent<Worker>();
            _workTable = workTableObjectReference.Value.GetComponent<WorkTable>();
        }

        public override NodeResult Execute()
        {
            return _worker.TryPutWood(_workTable)
                ? NodeResult.success
                : NodeResult.running;
        }
    }
}