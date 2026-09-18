using System;
using Zenject;

namespace SnakeGame
{
    public sealed class ChangeDifficultyController : IInitializable, IDisposable
    {
        private readonly CoinManager coinManager;
        private readonly IDifficulty difficulty;

        public ChangeDifficultyController(CoinManager coinManager, IDifficulty difficulty)
        {
            this.coinManager = coinManager;
            this.difficulty = difficulty;
        }

        public void Initialize()
        {
            coinManager.OnAllCoinPicked += ChangeDifficulty;
        }

        public void Dispose()
        {
            coinManager.OnAllCoinPicked -= ChangeDifficulty;
        }
        
        private void ChangeDifficulty()
        {
            difficulty.Next(out _);
        }

    }
}