using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TeamConfig", menuName = "Configs/TeamConfig")]
    public class TeamConfig : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<ETeam, Material> _teamMaterials;

        public Material GetTeamMaterial(ETeam team)
        {
            if (_teamMaterials.TryGetValue(team, out Material material))
                return material;

            throw new KeyNotFoundException($"Team \"{team}\" configuration does not exist");
        }
    }
}