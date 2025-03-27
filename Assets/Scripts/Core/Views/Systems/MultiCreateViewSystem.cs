using System;
using System.Collections.Generic;
using Entitas;
using Services;
using UnityEngine;

public interface IViewableEntity : IViewEntity, IAssetEntity, IEntity { }
public partial class GameEntity : IViewableEntity { }
public partial class UiEntity : IViewableEntity { }

namespace Core.Views.Systems
{
    public class MultiCreateViewSystem : MultiReactiveSystem<IViewableEntity, Contexts>
    {
      private readonly IAssetLoader _assetLoader;
      private readonly IGameObjectFactory _objectFactory;
      private readonly Dictionary<string, Transform> _parentByContextName;

      public MultiCreateViewSystem(
          Contexts contexts,
          IAssetLoader assetLoader,
          IGameObjectFactory objectFactory,
          Dictionary<string, Transform> parentByContextName) : base(contexts)
      {
          _assetLoader = assetLoader;
          _objectFactory = objectFactory;
          _parentByContextName = parentByContextName;
      }

      protected override ICollector[] GetTrigger(Contexts contexts)
      {
          return new ICollector[] {
              contexts.game.CreateCollector(GameMatcher.AllOf(GameMatcher.Asset).NoneOf(GameMatcher.View)),
              contexts.ui.CreateCollector(UiMatcher.AllOf(UiMatcher.Asset).NoneOf(UiMatcher.View)),
          };
      }

      protected override bool Filter(IViewableEntity entity)
      {
          return entity.hasAsset && !entity.hasView;
      }

      protected override void Execute(List<IViewableEntity> entities)
      {
          foreach (IViewableEntity entity in entities)
          {
              IEntityView entityView = CreateView(entity);
              entityView.Link(entity);
              entity.AddView(entityView);
          }
      }

      private IEntityView CreateView(IViewableEntity entity)
      {
          string contextName = entity.contextInfo.name;

          if (!_parentByContextName.TryGetValue(contextName, out Transform parent))
              throw new InvalidOperationException($"Parent transform for context:{contextName} was not found");

          string assetName = entity.asset.value;
          GameObject prefab = _assetLoader.LoadAsset(assetName);
          GameObject gameObject = _objectFactory.Instantiate(prefab, parent);

          if (gameObject.TryGetComponent(out IEntityView entityView))
              return entityView;

          throw new InvalidOperationException($"Instantiated object has no view component. Asset:{assetName}");
      }
    }
}