using UnityEngine;

namespace Engine.Configs
{
    [CreateAssetMenu(fileName = "SpawnArmyConfig", menuName = "Configs/SpawnArmyConfig")]
    public class SpawnArmyConfig : ScriptableObject
    {
        [SerializeField] private int _blueTeamCount;
        [SerializeField] private int _redTeamCount;
        [SerializeField] private Vector3 _blueTeamStartDirection;
        [SerializeField] private Vector3 _redTeamStartDirection;
        [SerializeField] private Vector3 _blueTeamStartSpawnPosition;
        [SerializeField] private Vector3 _redTeamStartSpawnPosition;
        [SerializeField] private float _spawnOffset;

        public int BlueTeamCount => _blueTeamCount;

        public int RedTeamCount => _redTeamCount;

        public Vector3 BlueTeamStartDirection => _blueTeamStartDirection;

        public Vector3 RedTeamStartDirection => _redTeamStartDirection;

        public Vector3 BlueTeamStartSpawnPosition => _blueTeamStartSpawnPosition;

        public Vector3 RedTeamStartSpawnPosition => _redTeamStartSpawnPosition;

        public float SpawnOffset => _spawnOffset;
    }
}