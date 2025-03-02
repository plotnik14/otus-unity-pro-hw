using UnityEngine;

namespace View
{
    public class UnitView : BattleView
    {
        [SerializeField] private Transform _firePoint;

        public Vector3 FirePoint => _firePoint.position;
    }
}