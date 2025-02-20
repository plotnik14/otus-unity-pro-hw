using System.Collections.Generic;
using Engine.View;
using Entitas;
using UnityEngine;

namespace Core.Systems
{
    public class CreateViewSystem : ReactiveSystem<GameEntity>
    {
        private readonly Transform _parent;

        public CreateViewSystem(IContext<GameEntity> context) : base(context)
        {
            _parent = new GameObject("Views").transform; // ToDo вынести в конфигурацию?
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
            GameObject prefab = Resources.Load<GameObject>(entity.asset.value); // ToDo вынести работу с ресурсами в отдельный сервис
            GameObject gameObject = Object.Instantiate(prefab, _parent); // ToDO тоже в сервис?
            var entityView = gameObject.GetComponent<IEntityView>();
            return entityView;
        }
    }
}