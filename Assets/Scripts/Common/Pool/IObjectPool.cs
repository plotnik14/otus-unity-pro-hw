using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IObjectPool<T> : IDisposable where T : Component, IReusable<T>
    {
        T Get();
    }
}