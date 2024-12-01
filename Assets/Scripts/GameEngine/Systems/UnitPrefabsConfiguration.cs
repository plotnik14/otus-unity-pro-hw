using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
    [CreateAssetMenu(fileName = "UnitPrefabsConfiguration", menuName = "Configuration/Create UnitPrefabsConfiguration")]
    public class UnitPrefabsConfiguration : ScriptableObject
    {
        [SerializeField] private List<Unit> _unitPrefabs;

        public Unit GetUnitPrefabByType(string type)
        {
            foreach (Unit unitPrefab in _unitPrefabs)
            {
                if (unitPrefab.Type == type)
                {
                    return unitPrefab;
                }
            }

            throw new ArgumentException($"No unit prefab for type:{type}");
        }
    }
}