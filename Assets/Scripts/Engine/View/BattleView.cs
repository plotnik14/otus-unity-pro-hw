using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Engine.View
{
    public class BattleView : UnityView, ITeamListener
    {
        // ToDO более оптимальный механизм. В отдельный класс?
        [SerializeField] private Material _blueMaterial;
        [SerializeField] private Material _redMaterial;
        [SerializeField] private List<Renderer> _renderers;

        protected override void OnLink(GameEntity linkedEntity)
        {
            base.OnLink(linkedEntity);
            linkedEntity.AddTeamListener(this);
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