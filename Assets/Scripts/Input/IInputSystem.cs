using System;

namespace ShootEmUp
{
    public interface IInputSystem
    {
        event Action OnFire;
        event Action OnMoveLeft;
        event Action OnMoveRight;
        event Action OnStopMovement;
    }
}