using System;
using Zenject;

namespace SnakeGame
{
    public sealed class SnakeSpeedController : IInitializable , IDisposable
    {
        private readonly IDifficulty _difficulty;
        private readonly ISnake _snake;

        public SnakeSpeedController(IDifficulty difficulty, ISnake snake)
        {
            _difficulty = difficulty;
            _snake = snake;
        }
        public void Initialize()
        {
            _difficulty.OnStateChanged += ChangeSpeed;
        }
        public void Dispose()
        {
            _difficulty.OnStateChanged -= ChangeSpeed;
        }
        
        private void ChangeSpeed()
        { 
            _snake.SetSpeed(_difficulty.Current);
        }
    }
}