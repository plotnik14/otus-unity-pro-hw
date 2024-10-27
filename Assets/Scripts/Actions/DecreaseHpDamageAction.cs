using UnityEngine;

namespace ShootEmUp
{
    public class DecreaseHpDamageAction : TakeDamageAction
    {
        [SerializeField] private HitPointsComponent _hpComponent;

        public override void TakeDamage(int damageValue)
        {
            _hpComponent.TakeDamage(damageValue);
        }
    }
}