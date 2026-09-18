using System;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SnakeOverBoundsGameOverController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IWorldBounds _worldBounds;
        private readonly GameCycle _gameCycle;

        public SnakeOverBoundsGameOverController(ISnake snake, IWorldBounds worldBounds, GameCycle gamecycle)
        {
            this._snake = snake;
            this._worldBounds = worldBounds;
            this._gameCycle = gamecycle;
        }

        public void Initialize()
        {
            _snake.OnMoved += InBounds;
        }
        public void Dispose()
        {
            _snake.OnMoved -= InBounds;
        }
        
        private void InBounds(Vector2Int position)
        {
            if (!_worldBounds.IsInBounds(position))
            {
                _gameCycle.FinishGame(false);
            }
        }
    }
}