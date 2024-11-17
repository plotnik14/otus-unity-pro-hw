using System;
using UnityEngine;

namespace SaveSystem.Data
{
    [Serializable]
    public class UnitData
    {
        public string ID;
        public string Type;
        public int HitPoints;
        public Vector3 Position;
        public Vector3 Rotation;
    }
}