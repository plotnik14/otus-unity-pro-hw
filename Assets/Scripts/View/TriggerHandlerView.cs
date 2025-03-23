using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace View
{
    public class TriggerHandlerView : MonoBehaviour
    {
        [FormerlySerializedAs("_unityView")] [SerializeField] private GameView gameView;

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            GameView otherGameView = other.gameObject.GetComponent<GameView>();
            GameEntity collidedWith = otherGameView.LinkedEntity;
            AddCollision(collidedWith);
        }
        
        private void AddCollision(GameEntity collidedWith)
        {
            List<GameEntity> collisionsList = GetOrCreateCollisionsList();
            collisionsList.Add(collidedWith);
        }

        private List<GameEntity> GetOrCreateCollisionsList()
        {
            GameEntity linkedEntity = gameView.LinkedEntity;

            if (linkedEntity.hasCollisions)
                return linkedEntity.collisions.list;

            List<GameEntity> collisionsList = new();
            linkedEntity.AddCollisions(collisionsList);
            return collisionsList;
        }
    }
}