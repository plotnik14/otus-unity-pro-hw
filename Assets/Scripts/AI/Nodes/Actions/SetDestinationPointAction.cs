using System;
using MBT;
using UnityEngine;

namespace AI.Nodes.Actions
{

    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Set Destination Point")]
    public class SetDestinationPointAction : Leaf
    {
        public TransformReference targetTransformReference = new(VarRefMode.DisableConstant);
        public Vector3Reference moveDestinationPointReference = new(VarRefMode.DisableConstant);

        public override NodeResult Execute()
        {
            if (targetTransformReference.Value == null)
                throw new InvalidOperationException("Target transform is not set");

            moveDestinationPointReference.Value = targetTransformReference.Value.position;
            return NodeResult.success;
        }
    }
}