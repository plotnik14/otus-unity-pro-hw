using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Engine.View
{
    public class UnityView : MonoBehaviour, IEntityView, IPositionListener, IRotationListener
    {
        private GameEntity _linkedEntity;

        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            _linkedEntity = (GameEntity)entity;
            _linkedEntity.AddPositionListener(this);
            _linkedEntity.AddRotationListener(this);
            OnLink(_linkedEntity);
        }

        public void OnPosition(GameEntity entity, Vector3 value) => gameObject.transform.position = value;

        public void OnRotation(GameEntity entity, Vector3 value) => gameObject.transform.forward = value;

        protected virtual void OnLink(GameEntity linkedEntity) { }

        protected virtual void OnDestroy()
        {
            gameObject.Unlink();
            _linkedEntity = null;
        }
    }
}