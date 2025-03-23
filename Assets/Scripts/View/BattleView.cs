using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace View
{
    public class BattleView : GameView, ITeamListener
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