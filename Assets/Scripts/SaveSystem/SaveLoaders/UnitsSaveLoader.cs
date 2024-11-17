using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveSystem.Data;
using SaveSystem.GameRepositories;
using UnityEngine;

namespace SaveSystem.SaveLoaders
{
    public class UnitsSaveLoader : SaveLoader<IEnumerable<UnitData>, UnitManager>
    {
        public UnitsSaveLoader(IGameRepository repository, UnitManager service) : base(repository, service) { }

        protected override IEnumerable<UnitData> ConvertToData(UnitManager service)
        {
            List<UnitData> unitsData = new();

            foreach (Unit unit in service.GetAllUnits())
            {
                var unitData = new UnitData
                {
                    ID = unit.ID,
                    Type = unit.Type,
                    HitPoints = unit.HitPoints,
                    Position = unit.Position,
                    Rotation = unit.Rotation,
                };
                unitsData.Add(unitData);
            }

            return unitsData;
        }

        protected override void SetupData(IEnumerable<UnitData> data, UnitManager service)
        {
            foreach (Unit unit in service.GetAllUnits())
            {
                RestoreUnitStateByData(unit, data);
            }
        }

        protected override void SetupDefaultData(UnitManager service)
        {
            service.SetupUnits(new HashSet<Unit>());
        }

        /// <summary>
        /// Восстановление характеристик, для существующих на сцене юнитов. (сейчас только HitPoints)
        /// </summary>
        private static void RestoreUnitStateByData(Unit unit, IEnumerable<UnitData> data)
        {
            UnitData unitData = data.FirstOrDefault(ud => ud.ID == unit.ID);

            if (unitData == null)
            {
                Debug.LogError($"Failed to restore state of unit with id:{unit.ID}");
                return;
            }

            unit.HitPoints = unitData.HitPoints;

            // Остальное сейчас ReadOnly. Нельзя восстановить без изменений в классе Unit
            // unit.Type = unitData.Type;
            // unit.Position = unitData.Position;
            // unit.Rotation = unitData.Rotation;
        }
    }
}