using Buildings;
using JetBrains.Annotations;
using UnityEngine;
using Tree = Trees.Tree;

namespace Units
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private MoveEngine _moveEngine;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private float _interactionDistance;

        private float _interactionDistanceSqr;

        public bool IsMoving => _moveEngine.IsMoving;

        [UsedImplicitly]
        private void Awake() => _interactionDistanceSqr = _interactionDistance * _interactionDistance;

        [UsedImplicitly]
        private void OnValidate() => _interactionDistanceSqr = _interactionDistance * _interactionDistance;

        public void MoveTo(Vector3 destinationPoint) => _moveEngine.MoveTo(destinationPoint);

        public bool TryChopTree(Tree tree)
        {
            if (!CanInteractWith(tree.transform))
                return false;

            tree.Chop();
            _inventory.WoodCount += tree.WoodCount;
            return true;
        }

        public bool TryPutWood(WorkTable workTable)
        {
            if (!CanInteractWith(workTable.transform))
                return false;

            workTable.AddWood(_inventory.WoodCount);
            _inventory.WoodCount = 0;
            return true;
        }

        private bool CanInteractWith(Transform target) => (target.position - transform.position).sqrMagnitude <= _interactionDistanceSqr;
    }
}