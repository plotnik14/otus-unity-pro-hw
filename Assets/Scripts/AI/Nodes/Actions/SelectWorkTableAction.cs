using MBT;
using UnityEngine;

namespace AI.Nodes.Actions
{

    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Select Work Table")]
    public class SelectWorkTableAction : Leaf
    {
        public TransformReference deliveryZoneTransformReference = new(VarRefMode.DisableConstant);
        public TransformReference targetTransformReference = new(VarRefMode.DisableConstant);

        public override NodeResult Execute()
        {
            targetTransformReference.Value = deliveryZoneTransformReference.Value;
            return NodeResult.success;
        }
    }
}