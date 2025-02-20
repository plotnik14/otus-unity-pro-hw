using Core.Systems;
using Engine.Services;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        private Entitas.Systems _systems;

        [UsedImplicitly]
        private void Start()
        {
            var contexts = Contexts.sharedInstance;

            _systems = new Feature("Systems")
                .Add(new SpawnArmySystem(contexts.game))
                .Add(new CreateViewSystem(contexts.game))
                .Add(new MovementSystem(contexts.game, new UnityTimeService())) // ToDO доделать создание сервиса
                .Add(new PositionEventSystem(contexts)); // ToDo вынести в конец пайплайна

            _systems.Initialize();
        }

        [UsedImplicitly]
        private void Update()
        {
            _systems.Execute();
            _systems.Cleanup();
        }
    }
}