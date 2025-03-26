using Entitas;
using Entitas.Unity;
using UnityEngine;
using View;

namespace UI
{
    public class UiEntityView : MonoBehaviour, IEntityView, IUiDestroyedListener
    {
        public UiEntity LinkedEntity { get; private set; }

        public void Link(IEntity entity)
        {
            gameObject.Link(entity);
            LinkedEntity = (UiEntity)entity;
            LinkedEntity.AddUiDestroyedListener(this);
            OnLink(LinkedEntity);
        }

        public void OnDestroyed(UiEntity entity)
        {
            gameObject.Unlink();
            Destroy(gameObject);
            LinkedEntity = null;
        }

        protected virtual void OnLink(UiEntity linkedEntity) { }
    }
}