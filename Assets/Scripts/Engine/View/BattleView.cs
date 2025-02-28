using System;
using System.Collections.Generic;
using Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Engine.View
{
    public class BattleView : UnityView, ITeamListener
    {
        // ToDO более оптимальный механизм. В отдельный класс?
        [SerializeField] private Material _blueMaterial;
        [SerializeField] private Material _redMaterial;
        [SerializeField] private List<Renderer> _renderers;

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
            switch (value)
            {
                case ETeam.BlueTeam:
                    SetMaterial(_blueMaterial);
                    break;
                case ETeam.RedTeam:
                    SetMaterial(_redMaterial);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
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