using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Core.Components
{
    [Game, Event(EventTarget.Self)]
    public class RotationComponent : IComponent
    {
        public Vector3 value;
    }
}