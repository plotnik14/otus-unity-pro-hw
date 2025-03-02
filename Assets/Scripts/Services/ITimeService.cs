namespace Services
{
    public interface ITimeService
    {
        /// <summary>
        /// Время, прошедшее с предыдущего кадра
        /// </summary>
        float DeltaTime { get; }

        /// <summary>
        /// Время, прошедшее со старта приложения
        /// </summary>
        float Time { get; }

        void Pause();

        void Resume();
    }
}