using JetBrains.Annotations;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _characterHitPoints;
        [SerializeField] private GameManager _gameManager;

        [UsedImplicitly]
        private void Start() => _characterHitPoints.OnDeath += OnDeath;

        [UsedImplicitly]
        private void OnDestroy() => _characterHitPoints.OnDeath -= OnDeath;

        private void OnDeath(HitPointsComponent _) => _gameManager.FinishGame();
    }
}