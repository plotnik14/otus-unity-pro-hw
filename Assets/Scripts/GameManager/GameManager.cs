using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        private readonly List<IGameLifecycleListener> _lifecycleListeners = new();
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();

        public EGameState State { get; private set; }

        [UsedImplicitly]
        private void Update()
        {
            if (State != EGameState.InProgress)
                return;

            float deltaTime = Time.deltaTime;

            for (var index = 0; index < _updateListeners.Count; index++)
            {
                _updateListeners[index].OnGameUpdate(deltaTime);
            }
        }

        [UsedImplicitly]
        private void FixedUpdate()
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

        [ContextMenu("Start Game")]
        public void StartGame()
        {
            State = EGameState.InProgress;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGameStartListener>()?.OnGameStart();
            }
        }

        [ContextMenu("Finish Game")]
        public void FinishGame()
        {
            State = EGameState.Finished;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGameFinishListener>()?.OnGameFinish();
            }

            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        [ContextMenu("Pause Game")]
        public void PauseGame()
        {
            State = EGameState.Paused;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGamePauseListener>()?.OnGamePause();
            }
        }

        [ContextMenu("Resume Game")]
        public void ResumeGame()
        {
            State = EGameState.InProgress;

            foreach (IGameLifecycleListener listener in _lifecycleListeners)
            {
                listener.As<IGameResumeListener>()?.OnGameResume();
            }
        }
    }
}