using System;

namespace ShootEmUp
{
    public interface IReusable<T>
    {
        /// <summary>
        /// Событие для возвращения объекта в пул
        /// </summary>
        event Action<T> OnInstanceReleased;
    }
}