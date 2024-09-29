using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class HitPointsComponent : MonoBehaviour
    {
        public event Action<HitPointsComponent> OnDeath;
        
        [SerializeField] private int _hitPoints;

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;

            if (_hitPoints <= 0)
            {
                OnDeath.SafeInvoke(this);
            }
        }
    }
}