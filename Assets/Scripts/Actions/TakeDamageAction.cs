using UnityEngine;

namespace ShootEmUp
{
    public abstract class TakeDamageAction : MonoBehaviour
    {
        public abstract void TakeDamage(int damageValue);
    }
}