using System;
using System.Collections.Generic;
using Entitas;
using Services;
using UnityEngine;
using View;

public interface IViewableEntity : IViewEntity, IAssetEntity, IEntity { }

public partial class GameEntity : IViewableEntity { }
public partial class GameStateEntity : IViewableEntity { }

namespace Core.Systems
{
    public class MultiCreateViewSystem : MultiReactiveSystem<IViewableEntity, Contexts>
    {
      private readonly IAssetLoader _assetLoader;
      private readonly IGameObjectFactory _objectFactory;
      private readonly Transform _parent;

      public MultiCreateViewSystem(
          Contexts contexts,
          IAssetLoader assetLoader,
          IGameObjectFactory objectFactory) : base(contexts)
      {
          _assetLoader = assetLoader;
          _objectFactory = objectFactory;
          _parent = new GameObject("Views").transform;
      }

      protected override ICollector[] GetTrigger(Contexts contexts)
      {
          return new ICollector[] {
              contexts.game.CreateCollector(GameMatcher.Asset),
              contexts.gameState.CreateCollector(GameStateMatcher.Asset),
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
          string assetName = entity.asset.value;
          GameObject prefab = _assetLoader.LoadAsset(assetName);
          GameObject gameObject = _objectFactory.Instantiate(prefab, _parent);

          if (gameObject.TryGetComponent(out IEntityView entityView))
              return entityView;

          throw new InvalidOperationException($"Instantiated object has no view component. Asset:{assetName}");
      }
    }
}