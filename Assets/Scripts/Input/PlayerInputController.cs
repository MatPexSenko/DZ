using SnakeGame;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class PlayerInputController : ITickable
    {
        private readonly ISnake snake;

        public PlayerInputController(ISnake snake)
        {
            this.snake = snake;
        }

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
                snake.Turn(SnakeDirection.UP);
            
            if(Input.GetKeyDown(KeyCode.DownArrow))
                snake.Turn(SnakeDirection.DOWN);
            
            if(Input.GetKeyDown(KeyCode.RightArrow))
                snake.Turn(SnakeDirection.RIGHT);
            
            if(Input.GetKeyDown(KeyCode.LeftArrow))
                snake.Turn(SnakeDirection.LEFT);
        }
    }
}