using UnityEngine;

namespace ShootEmUp
{
    public class TimeScaleService : ITimeScaleService
    {
        private const float PAUSE_SCALE_VALUE = 0f;
        private const float IN_PROGRESS_SCALE_VALUE = 1f;

        public void PauseTime() => Time.timeScale = PAUSE_SCALE_VALUE;

        public void ResumeTime() => Time.timeScale = IN_PROGRESS_SCALE_VALUE;
    }
}