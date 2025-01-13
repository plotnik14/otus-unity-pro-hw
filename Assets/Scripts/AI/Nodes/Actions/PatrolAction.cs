using AI.Brains;
using JetBrains.Annotations;
using MBT;
using UnityEngine;

namespace AI.Nodes.Actions
{
    [AddComponentMenu("")]
    [MBTNode(name = "Actions/Patrol")]
    public class PatrolAction : Leaf
    {
        public GameObjectReference workerBrainObjectReference = new(VarRefMode.DisableConstant);
        public GameObjectReference patrolPointsObjectReference = new(VarRefMode.DisableConstant);

        private WorkerBrain _workerBrain;
        private PatrolPoints _patrolPoints;

        [UsedImplicitly]
        private void Awake()
        {
            _workerBrain = workerBrainObjectReference.Value.GetComponent<WorkerBrain>();
            _patrolPoints = patrolPointsObjectReference.Value.GetComponent<PatrolPoints>();
        }

        public override NodeResult Execute()
        {
            if (_workerBrain.IsOnPoint(_patrolPoints.CurrentPoint))
            {
                _patrolPoints.NextPoint();
            }

            _workerBrain.MoveToPoint(_patrolPoints.CurrentPoint);
            return NodeResult.success;
        }
    }
}