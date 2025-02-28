using System;
using System.Collections.Generic;
using Engine.Services;
using Engine.View;
using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class CreateViewSystem : ReactiveSystem<GameEntity>
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IGameObjectFactory _objectFactory;
        private readonly Transform _parent;

        public CreateViewSystem(
            IContext<GameEntity> context,
            IAssetLoader assetLoader,
            IGameObjectFactory objectFactory) : base(context)
        {
            _assetLoader = assetLoader;
            _objectFactory = objectFactory;
            _parent = new GameObject("Views").transform;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Asset);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAsset && !entity.hasEntityView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity entity in entities)
            {
                IEntityView entityView = CreateView(entity);
                entityView.Link(entity);
                entity.AddEntityView(entityView);
            }
        }

        private IEntityView CreateView(GameEntity entity)
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