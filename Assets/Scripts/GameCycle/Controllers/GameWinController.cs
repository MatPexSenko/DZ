using System;
using Zenject;

namespace SnakeGame
{
    public sealed class GameWinController : IInitializable, IDisposable
    {
        private readonly IDifficulty difficulty;
        private readonly CoinManager coinManager;
        private readonly GameCycle gameCycle;

        public GameWinController(IDifficulty difficulty, CoinManager coinManager, GameCycle gameCycle)
        {
            this.difficulty = difficulty;
            this.coinManager = coinManager;
            this.gameCycle = gameCycle;
        }

        public void Initialize()
        {
            coinManager.OnAllCoinPicked += CheckWin;
        }
        private void CheckWin()
        {
            if (difficulty.Current == difficulty.Max)
            {
                gameCycle.FinishGame(true);
            }
        }
        public void Dispose()
        {
            coinManager.OnAllCoinPicked -= CheckWin;
        }
    }
}