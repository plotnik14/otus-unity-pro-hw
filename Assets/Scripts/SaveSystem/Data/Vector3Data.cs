using UnityEngine;

namespace SaveSystem.Data
{
    
    public class Vector3Data
    {
        public float X;
        public float Y;
        public float Z;

        public static Vector3Data FromVector3(Vector3 vector)
        {
            return new Vector3Data
            {
                X = vector.x,
                Y = vector.y,
                Z = vector.z
            };
        }

        public static Vector3 ToVector3(Vector3Data vectorData)
        {
            return new Vector3(vectorData.X, vectorData.Y, vectorData.Z);
        }
    }
}