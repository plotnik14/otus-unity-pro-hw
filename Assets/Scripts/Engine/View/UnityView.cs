using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using JetBrains.Annotations;
using UnityEngine;

namespace Engine.View
{
    public class UnityView : MonoBehaviour, IEntityView, IPositionListener, IRotationListener, IDestroyedListener
    {
        private GameEntity _linkedEntity;

        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            _linkedEntity = (GameEntity)entity;
            _linkedEntity.AddPositionListener(this);
            _linkedEntity.AddRotationListener(this);
            _linkedEntity.AddDestroyedListener(this);
            OnLink(_linkedEntity);
        }

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            UnityView unityView = other.gameObject.GetComponent<UnityView>();
            GameEntity collidedWith = unityView._linkedEntity;
            AddCollision(collidedWith);
        }

        public void OnPosition(GameEntity entity, Vector3 value) => gameObject.transform.position = value;

        public void OnRotation(GameEntity entity, Vector3 value) => gameObject.transform.forward = value;

        public void OnDestroyed(GameEntity entity)
        {
            gameObject.Unlink();
            Destroy(gameObject);
            _linkedEntity = null;
        }

        protected virtual void OnLink(GameEntity linkedEntity) { }

        private void AddCollision(GameEntity collidedWith)
        {
            List<GameEntity> collisionsList = GetOrCreateCollisionsList();
            collisionsList.Add(collidedWith);
        }

        private List<GameEntity> GetOrCreateCollisionsList()
        {
            if (_linkedEntity.hasCollisions)
                return _linkedEntity.collisions.list;

            List<GameEntity> collisionsList = new();
            _linkedEntity.AddCollisions(collisionsList);
            return collisionsList;
        }
    }
}