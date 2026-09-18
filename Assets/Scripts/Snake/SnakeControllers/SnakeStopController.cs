using System;
using Zenject;

namespace SnakeGame
{
    public sealed class SnakeStopController : IInitializable , IDisposable
    {
        private readonly GameCycle _gameCycle;
        private readonly ISnake _snake;

        public SnakeStopController(GameCycle gameCycle, ISnake snake)
        {
            _gameCycle = gameCycle;
            _snake = snake;
        }

        public void Initialize()
        {
            _gameCycle.OnGameFinished += ChangeSpeed;
        }
        public void Dispose()
        {
            _gameCycle.OnGameFinished -= ChangeSpeed;
        }
        
        private void ChangeSpeed(bool _)
        {
            _snake.SetActive(false);
        }
    }
}