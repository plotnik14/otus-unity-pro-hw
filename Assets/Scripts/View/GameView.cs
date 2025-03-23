using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace View
{
    public class GameView : MonoBehaviour, IEntityView, IPositionListener, IRotationListener, IGameDestroyedListener
    {
        public GameEntity LinkedEntity { get; private set; }

        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            LinkedEntity = (GameEntity)entity;
            LinkedEntity.AddPositionListener(this);
            LinkedEntity.AddRotationListener(this);
            LinkedEntity.AddGameDestroyedListener(this);
            OnLink(LinkedEntity);
        }

        public void OnPosition(GameEntity entity, Vector3 value) => gameObject.transform.position = value;

        public void OnRotation(GameEntity entity, Vector3 value) => gameObject.transform.forward = value;

        public void OnDestroyed(GameEntity entity)
        {
            gameObject.Unlink();
            Destroy(gameObject);
            LinkedEntity = null;
        }

        protected virtual void OnLink(GameEntity linkedEntity) { }
    }
}