using System.Collections.Generic;
using UnityEngine;
using Utils;
using VContainer.Unity;

namespace ShootEmUp
{
    public class GameManager : IGameManager, ITickable, IFixedTickable, INonLazy
    {
        private readonly List<IGameLifecycleListener> _lifecycleListeners = new();
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        private readonly ITimeScaleService _timeScaleService;

        public EGameState State { get; private set; }

        public GameManager(ITimeScaleService timeScaleService) => _timeScaleService = timeScaleService;

        public void Tick()
        {
            if (State != EGameState.InProgress)
                return;

            float deltaTime = Time.deltaTime;

            for (var index = 0; index < _updateListeners.Count; index++)
            {
                _updateListeners[index].OnGameUpdate(deltaTime);
            }
        }

        public void FixedTick()
        {
            if (State != EGameState.InProgress)
                return;

            float deltaTime = Time.fixedDeltaTime;

            for (var index = 0; index < _fixedUpdateListeners.Count; index++)
            {
                _fixedUpdateListeners[index].OnGameFixedUpdate(deltaTime);
            }
        }

        public void RegisterListener(IGameLifecycleListener listener)
        {
            _lifecycleListeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
                _updateListeners.Add(updateListener);

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _fixedUpdateListeners.Add(fixedUpdateListener);
        }

        public void UnregisterListener(IGameLifecycleListener listener)
        {
            _lifecycleListeners.Remove(listener);

            if (listener is IGameUpdateListener updateListener)
                _updateListeners.Remove(updateListener);

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _fixedUpdateListeners.Remove(fixedUpdateListener);
        }

        [ContextMenu("Start Game")]
        public void StartGame()
        {
            _timeScaleService.ResumeTime();
            State = EGameState.InProgress;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGameStartListener>()?.OnGameStart();
            }
        }

        [ContextMenu("Finish Game")]
        public void FinishGame()
        {
            _timeScaleService.PauseTime();
            State = EGameState.Finished;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGameFinishListener>()?.OnGameFinish();
            }

            Debug.Log("Game over!");
        }

        [ContextMenu("Pause Game")]
        public void PauseGame()
        {
            _timeScaleService.PauseTime();
            State = EGameState.Paused;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGamePauseListener>()?.OnGamePause();
            }
        }

        [ContextMenu("Resume Game")]
        public void ResumeGame()
        {
            _timeScaleService.ResumeTime();
            State = EGameState.InProgress;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGameResumeListener>()?.OnGameResume();
            }
        }
    }
}