using System;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace SnakeGame
{
    public class GameCycle
    {
        public event Action OnGameStarted;
        public event Action<bool> OnGameFinished;

        public bool IsStarted { get; private set; }

        public void StartGame()
        {
            if (!this.IsStarted)
            {
                this.IsStarted = true;
                this.OnGameStarted?.Invoke();
                Debug.Log("Game Started!");
            }
        }

        public void FinishGame(bool win)
        {
            if (this.IsStarted)
            {
                this.IsStarted = false;
                this.OnGameFinished?.Invoke(win);
                Debug.Log("Game Finished!");
            }
        }
    }
}