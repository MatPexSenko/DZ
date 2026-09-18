using System;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public sealed class CoinPickUpController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly CoinManager _coinManager;

        public CoinPickUpController(ISnake snake, CoinManager coinManager)
        {
            this._snake = snake;
            this._coinManager = coinManager;
        }

        public void Initialize()
        {
            _snake.OnMoved += PickCoin;
        }

        public void Dispose()
        {
            _snake.OnMoved -= PickCoin;
        }

        public void PickCoin(Vector2Int _)
        {
            if (_coinManager.Coins.TryGetValue(_snake.HeadPosition, out var coin))
            {
                _coinManager.DespawnCoin(coin);
            }
        }
    }
}