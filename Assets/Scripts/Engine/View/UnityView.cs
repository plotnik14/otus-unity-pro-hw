using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Engine.View
{
    public class UnityView : MonoBehaviour, IEntityView, IPositionListener, IRotationListener
    {
        protected GameEntity LinkedEntity { get; private set; }

        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            LinkedEntity = (GameEntity)entity;
            LinkedEntity.AddPositionListener(this);
            LinkedEntity.AddRotationListener(this);
        }

        public void OnPosition(GameEntity entity, Vector3 value) => gameObject.transform.position = value;

        public void OnRotation(GameEntity entity, Vector3 value) => gameObject.transform.forward = value;

        protected virtual void OnDestroy()
        {
            gameObject.Unlink();
            LinkedEntity = null;
        }
    }
}