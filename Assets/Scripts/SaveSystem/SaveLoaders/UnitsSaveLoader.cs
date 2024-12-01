using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveSystem.Data;
using SaveSystem.GameRepositories;

namespace SaveSystem.SaveLoaders
{
    public class UnitsSaveLoader : SaveLoader<IEnumerable<UnitData>, UnitManager>
    {
        private readonly UnitPrefabsConfiguration _unitPrefabsConfiguration;

        public UnitsSaveLoader(
            IGameStateRepository repository,
            UnitManager service,
            UnitPrefabsConfiguration unitPrefabsConfiguration)
            : base(repository, service)
        {
            _unitPrefabsConfiguration = unitPrefabsConfiguration;
        }

        protected override IEnumerable<UnitData> ConvertToData(UnitManager service)
        {
            List<UnitData> unitsData = new();

            foreach (Unit unit in service.GetAllUnits())
            {
                var unitData = new UnitData
                {
                    Type = unit.Type,
                    HitPoints = unit.HitPoints,
                    Position = Vector3Data.FromVector3(unit.Position),
                    Rotation = Vector3Data.FromVector3(unit.Rotation),
                };
                unitsData.Add(unitData);
            }

            return unitsData;
        }

        protected override void SetupData(IEnumerable<UnitData> data, UnitManager service)
        {
            DestroyExistingUnits(service);
            CreateUnitsByData(data, service);
        }

        protected override void SetupDefaultData(UnitManager service)
        {
            DestroyExistingUnits(service);
            service.SetupUnits(new HashSet<Unit>());
        }

        private static void DestroyExistingUnits(UnitManager service)
        {
            foreach (Unit unit in service.GetAllUnits().ToList())
            {
                service.DestroyUnit(unit);
            }
        }

        private void CreateUnitsByData(IEnumerable<UnitData> data, UnitManager service)
        {
            foreach (UnitData unitData in data)
            {
                Unit prefab = _unitPrefabsConfiguration.GetUnitPrefabByType(unitData.Type);

                Unit unit = service.SpawnUnit(prefab,
                    Vector3Data.ToVector3(unitData.Position),
                    Vector3Data.ToQuaternion(unitData.Rotation));

                unit.HitPoints = unitData.HitPoints;
            }
        }
    }
}