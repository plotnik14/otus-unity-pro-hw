using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Engine.View
{
    public class UnityView : MonoBehaviour, IEntityView,
        IPositionListener, IRotationListener, IDestroyedListener
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

        public void OnPosition(GameEntity entity, Vector3 value) => gameObject.transform.position = value;

        public void OnRotation(GameEntity entity, Vector3 value) => gameObject.transform.forward = value;

        public void OnDestroyed(GameEntity entity)
        {
            gameObject.Unlink();
            Destroy(gameObject);
            _linkedEntity = null;
        }

        protected virtual void OnLink(GameEntity linkedEntity) { }
    }
}