using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private float _attackCooldown;

        private GameObject _target;
        private CancellationTokenSource _attackCts;

        [UsedImplicitly]
        private void OnDestroy()
        {
            _attackCts.CancelAndDispose();
            _attackCts = null;
        }

        public void StartAttackingTarget(GameObject target)
        {
            _target = target;
            _attackCts.CancelAndDispose();
            _attackCts = new CancellationTokenSource();
            AttackTargetByCooldownAsync(_attackCts.Token).Forget();
        }

        public void StopAttackingTarget()
        {
            _attackCts.CancelAndDispose();
            _attackCts = null;
            _target = null;
        }

        private async UniTaskVoid AttackTargetByCooldownAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(_attackCooldown, cancellationToken: cancellationToken);
                Vector2 direction = (_target.transform.position - transform.position).normalized;
                _weapon.Fire(direction);
            }
        }
    }
}