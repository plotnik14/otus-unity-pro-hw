using Entitas;
using UnityEngine;

namespace Core.Components
{
    [Game]
    public class RotationComponent : IComponent
    {
        public Quaternion value;
    }
}