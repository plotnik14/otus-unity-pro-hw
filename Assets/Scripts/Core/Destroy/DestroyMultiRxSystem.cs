using System.Collections.Generic;
using Entitas;

public interface IDestroyableEntity : IEntity, IDestroyedEntity { }
public partial class GameEntity : IDestroyableEntity { }
public partial class GameStateEntity : IDestroyableEntity { }
public partial class UiEntity : IDestroyableEntity { }

namespace Core.Destroy
{
    public class DestroyMultiRxSystem : MultiReactiveSystem<IDestroyableEntity, Contexts>
    {
        public DestroyMultiRxSystem(Contexts contexts) : base(contexts) { }

        protected override ICollector[] GetTrigger(Contexts contexts)
        {
            return new ICollector[] {
                contexts.game.CreateCollector(GameMatcher.Destroyed),
                contexts.gameState.CreateCollector(GameStateMatcher.Destroyed),
                contexts.ui.CreateCollector(UiMatcher.Destroyed),
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