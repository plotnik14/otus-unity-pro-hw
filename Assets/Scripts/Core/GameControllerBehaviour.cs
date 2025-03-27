using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameControllerBehaviour : MonoBehaviour
    {
        private GameController _gameController;

        [Inject]
        private void Construct(GameController gameController) => _gameController = gameController;

        [UsedImplicitly]
        private void Start() => _gameController.Initialize();

        [UsedImplicitly]
        private void Update() => _gameController.Execute();

        [UsedImplicitly]
        private void OnDestroy() => _gameController.TearDown();
    }
}