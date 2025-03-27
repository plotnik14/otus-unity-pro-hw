using UnityEngine;

namespace Core.Views.Game
{
    public class UnitView : BattleView
    {
        [SerializeField] private Transform _firePoint;

        public Vector3 FirePoint => _firePoint.position;
    }
}