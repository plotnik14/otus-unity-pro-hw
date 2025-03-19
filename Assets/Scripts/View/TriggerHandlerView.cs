using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace View
{
    public class TriggerHandlerView : MonoBehaviour
    {
        [SerializeField] private UnityView _unityView;

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            UnityView otherUnityView = other.gameObject.GetComponent<UnityView>();
            GameEntity collidedWith = otherUnityView.LinkedEntity;
            AddCollision(collidedWith);
        }
        
        private void AddCollision(GameEntity collidedWith)
        {
            List<GameEntity> collisionsList = GetOrCreateCollisionsList();
            collisionsList.Add(collidedWith);
        }

        private List<GameEntity> GetOrCreateCollisionsList()
        {
            GameEntity linkedEntity = _unityView.LinkedEntity;

            if (linkedEntity.hasCollisions)
                return linkedEntity.collisions.list;

            List<GameEntity> collisionsList = new();
            linkedEntity.AddCollisions(collisionsList);
            return collisionsList;
        }
    }
}