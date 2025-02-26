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
            ITimeService timeService = new UnityTimeService(); // ToDO DI?

            var contexts = Contexts.sharedInstance;
            _systems = new Feature("Systems")
                .Add(new SpawnArmySystem(contexts.game))


                .Add(new MovementSystem(contexts.game, timeService))
                .Add(new RotationSystem(contexts.game, timeService))


                .Add(new FindTargetSystem(contexts.game))
                .Add(new AttackCooldownSystem(contexts.game, timeService))
                .Add(new AttackSystem(contexts.game))


                .Add(new CreateViewSystem(contexts.game))


                .Add(new PositionEventSystem(contexts))
                .Add(new RotationEventSystem(contexts))
                .Add(new TeamEventSystem(contexts));


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