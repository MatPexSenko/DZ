using System;
using Zenject;

namespace SnakeGame
{
    public class SnakeExpandController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly CoinManager _coinManager;

        public SnakeExpandController(ISnake snake, CoinManager coinManager)
        {
            this._snake = snake;
            this._coinManager = coinManager;
        }
        public void Initialize()
        {
            _coinManager.OnCoinPicked += ExpandSnake;
        }
        public void Dispose()
        {
            _coinManager.OnCoinPicked -= ExpandSnake;
        }
        
        private void ExpandSnake(ICoin coin)
        {
            _snake.Expand(coin.Bones);
        }

    }
}