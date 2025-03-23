using System.Collections.Generic;
using Entitas;

public interface IDestroyableEntity : IEntity, IDestroyedEntity { }
public partial class GameEntity : IDestroyableEntity { }
public partial class GameStateEntity : IDestroyableEntity { }

namespace Core.Systems
{
    public class MultiDestroySystem : MultiReactiveSystem<IDestroyableEntity, Contexts>
    {
        public MultiDestroySystem(Contexts contexts) : base(contexts) { }

        protected override ICollector[] GetTrigger(Contexts contexts)
        {
            return new ICollector[] {
                contexts.game.CreateCollector(GameMatcher.Destroyed),
                contexts.gameState.CreateCollector(GameStateMatcher.Destroyed),
            };
        }

        protected override bool Filter(IDestroyableEntity entity) => entity.isDestroyed;

        protected override void Execute(List<IDestroyableEntity> entities)
        {
            foreach (IDestroyableEntity entity in entities)
            {
                entity.Destroy();
            }
        }
    }
}