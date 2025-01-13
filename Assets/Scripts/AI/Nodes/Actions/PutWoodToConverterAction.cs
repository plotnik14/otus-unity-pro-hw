using AI.Brains;
using Buildings;
using JetBrains.Annotations;
using MBT;
using UnityEngine;

namespace AI.Nodes.Actions
{
    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Put Wood To Converter")]
    public class PutWoodToConverterAction : Leaf
    {
        public GameObjectReference workerBrainObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference workTableObjectReference = new(VarRefMode.DisableConstant);

        private WorkerBrain _workerBrain;
        private WorkTable _workTable;

        [UsedImplicitly]
        private void Awake()
        {
            _workerBrain = workerBrainObjectReference.Value.GetComponent<WorkerBrain>();
            _workTable = workTableObjectReference.Value.GetComponent<WorkTable>();
        }

        public override NodeResult Execute()
        {
            return _workerBrain.TryPutWood(_workTable)
                ? NodeResult.success
                : NodeResult.running;
        }
    }
}