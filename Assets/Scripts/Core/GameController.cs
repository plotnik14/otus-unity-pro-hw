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
            // ToDO DI?
            ITimeService timeService = new UnityTimeService();


            var contexts = Contexts.sharedInstance;
            _systems = new Feature("Systems")

                .Add(new SpawnArmySystem(contexts.game))
                .Add(new MovementSystem(contexts.game, timeService))
                .Add(new RotationSystem(contexts.game, timeService))
                .Add(new UnitCollisionSystem(contexts.game))
                .Add(new ProjectileCollisionSystem(contexts.game))
                .Add(new DealDamageSystem(contexts.game))
                .Add(new ReleaseTargetSystem(contexts.game))
                .Add(new FindTargetSystem(contexts.game))
                .Add(new AttackCooldownSystem(contexts.game, timeService))
                .Add(new AttackSystem(contexts.game))

                // View
                .Add(new CreateViewSystem(contexts.game))

                // Events (Generated)
                .Add(new DestroyedEventSystem(contexts))
                .Add(new PositionEventSystem(contexts))
                .Add(new RotationEventSystem(contexts))
                .Add(new TeamEventSystem(contexts))

                // CleanUp
                .Add(new DestroyedCleanupSystem(contexts.game));


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