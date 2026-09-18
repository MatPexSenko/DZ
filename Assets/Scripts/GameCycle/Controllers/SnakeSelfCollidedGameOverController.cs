using System;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SnakeSelfCollidedGameOverController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly GameCycle _gameCycle;

        public SnakeSelfCollidedGameOverController(ISnake snake, GameCycle gameCycle)
        {
            this._snake = snake;
            this._gameCycle = gameCycle;
        }

        public void Initialize()
        {
            _snake.OnSelfCollided += SelfDestructed;
        }
        public void Dispose()
        {
            _snake.OnSelfCollided -= SelfDestructed;
        }
        
        private void SelfDestructed()
        {
            _gameCycle.FinishGame(false);
        }
    }
}