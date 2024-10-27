using System;

namespace ShootEmUp
{
    public class CharacterDeathObserver : IDisposable, INonLazy
    {
        private readonly IGameManager _gameManager;
        private readonly HitPointsComponent _characterHitPoints;

        public CharacterDeathObserver(IGameManager gameManager, HitPointsComponent characterHitPoints)
        {
            _gameManager = gameManager;
            _characterHitPoints = characterHitPoints;

            _characterHitPoints.OnDeath += OnDeath;
        }

        public void Dispose() => _characterHitPoints.OnDeath -= OnDeath;

        private void OnDeath(HitPointsComponent _) => _gameManager.FinishGame();
    }
}