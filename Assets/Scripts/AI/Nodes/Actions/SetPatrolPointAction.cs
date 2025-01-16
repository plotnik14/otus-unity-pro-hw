using JetBrains.Annotations;
using MBT;
using Units;
using UnityEngine;

namespace AI.Nodes.Actions
{
    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Set Patrol Point")]
    public class SetPatrolPointAction : Leaf
    {
        public GameObjectReference workerObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference patrolPointsObjectReference = new(VarRefMode.DisableConstant);
        public Vector3Reference moveDestinationPointReference = new(VarRefMode.DisableConstant);

        private Worker _worker;
        private PatrolPoints _patrolPoints;

        [UsedImplicitly]
        private void Awake()
        {
            _worker = workerObjectReference.Value.GetComponent<Worker>();
            _patrolPoints = patrolPointsObjectReference.Value.GetComponent<PatrolPoints>();
        }

        public override NodeResult Execute()
        {
            if (!_worker.IsMoving)
            {
                _patrolPoints.NextPoint();
                moveDestinationPointReference.Value = _patrolPoints.CurrentPoint;
            }

            return NodeResult.success;
        }
    }
}