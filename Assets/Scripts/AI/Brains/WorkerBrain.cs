using Buildings;
using Units;
using UnityEngine;
using Tree = Trees.Tree;

namespace AI.Brains
{
    public class WorkerBrain : MonoBehaviour
    {
        [SerializeField] private Worker _worker;
        [SerializeField] private float _reachTargetDistance = 0.1f;

        public bool IsOnPoint(Vector3 point) => (point - _worker.transform.position).sqrMagnitude < _reachTargetDistance * _reachTargetDistance;

        public void MoveToPoint(Vector3 targetPoint)
        {
            Vector3 direction = (targetPoint - _worker.transform.position).normalized;
            _worker.SetDirection(direction);
        }

        public void StopMovement() => _worker.SetDirection(Vector3.zero);

        public bool TryChopTree(Tree tree)
        {
            if (!IsOnPoint(tree.transform.position))
                return false;

            _worker.WoodCount += 1;
            return true;
        }

        public bool TryPutWood(WorkTable workTable)
        {
            if (!IsOnPoint(workTable.transform.position))
                return false;

            workTable.AddWood(_worker.WoodCount);
            _worker.WoodCount = 0;
            return true;

        }
    }
}