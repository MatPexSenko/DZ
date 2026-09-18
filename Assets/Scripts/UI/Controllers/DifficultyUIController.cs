using System;
using Zenject;

namespace SnakeGame
{
    public sealed class DifficultyUIController : IInitializable , IDisposable
    {
        private readonly IDifficulty difficulty;
        private readonly IGameUI gameUI;


        public DifficultyUIController(IDifficulty difficulty, IGameUI gameUI)
        {
            this.difficulty = difficulty;
            this.gameUI = gameUI;
        }

        public void Initialize()
        {
            difficulty.OnStateChanged += ChangeUI;
        }

        private void ChangeUI()
        {
            gameUI.SetDifficulty(difficulty.Current, difficulty.Max);
        }

        public void Dispose()
        {
            difficulty.OnStateChanged -= ChangeUI;
        }
    }
}