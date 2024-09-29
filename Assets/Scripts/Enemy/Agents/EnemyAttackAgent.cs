using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private float _attackCooldown;

        private GameObject _target;
        private Coroutine _attackingCoroutine;

        public void StartAttackingTarget(GameObject target)
        {
            _target = target;
            _attackingCoroutine = StartCoroutine(AttackTargetByCooldown());
        }

        public void StopAttackingTarget()
        {
            if (_attackingCoroutine != null)
            {
                StopCoroutine(_attackingCoroutine);
            }

            _target = null;
            _attackingCoroutine = null;
        }

        private IEnumerator AttackTargetByCooldown()
        {
            while (true)
            {
                yield return new WaitForSeconds(_attackCooldown);

                Vector2 direction = (_target.transform.position - transform.position).normalized;
                _weapon.Fire(direction);
            }
        }
    }
}