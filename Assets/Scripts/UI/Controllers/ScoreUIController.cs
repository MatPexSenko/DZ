using System;
using Zenject;

namespace SnakeGame
{
    public sealed class ScoreUIController : IInitializable , IDisposable
    {
        private readonly CoinManager coinManager;
        private readonly IGameUI gameUI;

        public ScoreUIController(CoinManager coinManager, IGameUI gameUI)
        {
            this.coinManager = coinManager;
            this.gameUI = gameUI;
            
            gameUI.SetScore("0");
        }

        public void Initialize()
        {
            coinManager.OnCoinPicked += ChangeUI;
        }

        private void ChangeUI(ICoin _)
        {
            gameUI.SetScore(coinManager.TotalScore.ToString());
        }

        public void Dispose()
        {
            coinManager.OnCoinPicked -= ChangeUI;
        }
    }
}