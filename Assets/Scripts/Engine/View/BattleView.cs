using System.Collections.Generic;
using Core;
using Engine.Configs;
using UnityEngine;

namespace Engine.View
{
    public class BattleView : UnityView, ITeamListener
    {
        [SerializeField] private List<Renderer> _renderers;
        [SerializeField] private TeamConfig _teamConfig;

        protected override void OnLink(GameEntity linkedEntity)
        {
            base.OnLink(linkedEntity);
            linkedEntity.AddTeamListener(this);
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