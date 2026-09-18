using System;
using Zenject;

namespace SnakeGame
{
    public class GameOverUIController : IInitializable, IDisposable
    {
        private readonly GameCycle gameCycle;
        private readonly IGameUI gameUI;

        public GameOverUIController(GameCycle gameCycle, IGameUI gameUI)
        {
            this.gameCycle = gameCycle;
            this.gameUI = gameUI;
        }

        public void Initialize()
        {
            gameCycle.OnGameFinished += Popup;
        }
        public void Dispose()
        {
            gameCycle.OnGameFinished -= Popup;
        }
        
        private void Popup(bool win)
        {
            gameUI.GameOver(win);
        }
    }
}