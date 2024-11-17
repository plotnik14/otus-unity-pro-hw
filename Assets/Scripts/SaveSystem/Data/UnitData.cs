using System;

namespace SaveSystem.Data
{
    [Serializable]
    public class UnitData
    {
        public string ID;
        public string Type;
        public int HitPoints;
        public Vector3Data Position;
        public Vector3Data Rotation;
    }
}