using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class GameStartUp : MonoBehaviour
    {
        private  GameCycle gameCycle;
        private IDifficulty difficulty;
        
        [Inject]
        private void Construct(GameCycle gameCycle, IDifficulty difficulty)
        {
            this.gameCycle = gameCycle;
            this.difficulty = difficulty;
        }
        private void Start()
        {
            difficulty.Next(out _);
            this.gameCycle.StartGame();
        }
    }
}