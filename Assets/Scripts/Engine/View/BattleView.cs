using System.Collections.Generic;
using Core;
using Engine.Configs;
using JetBrains.Annotations;
using UnityEngine;

namespace Engine.View
{
    public class BattleView : UnityView, ITeamListener
    {
        [SerializeField] private List<Renderer> _renderers;
        [SerializeField] private TeamConfig _teamConfig;

        private GameEntity _linkedEntity;

        protected override void OnLink(GameEntity linkedEntity)
        {
            base.OnLink(linkedEntity);
            _linkedEntity = linkedEntity;
            _linkedEntity.AddTeamListener(this);
        }

        [UsedImplicitly]
        private void OnTriggerEnter(Collider other)
        {
            BattleView battleView = other.gameObject.GetComponent<BattleView>();
            GameEntity collidedWith = battleView._linkedEntity;

            // ToDo в отдельный компонент? Перенести на уровень UnityView?
            if (_linkedEntity.hasCollisions)
            {
                List<GameEntity> collisionsList = _linkedEntity.collisions.list;
                collisionsList.Add(collidedWith);
                return;
            }

            List<GameEntity> collisionsList2 = new List<GameEntity>(); // ToDo рефакторинг!
            collisionsList2.Add(collidedWith);
            _linkedEntity.AddCollisions(collisionsList2);
        }

        public void OnTeam(GameEntity entity, ETeam value)
        {
            Material teamMaterial = _teamConfig.GetTeamMaterial(value);
            SetMaterial(teamMaterial);
        }

        private void SetMaterial(Material material)
        {
            foreach (Renderer objectRenderer in _renderers)
            {
                objectRenderer.material = material;
            }
        }
    }
}