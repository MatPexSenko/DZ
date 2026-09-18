using SnakeGame;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class GameContext : MonoInstaller
    {
        [SerializeField] private int levels = 9;
        
        [SerializeField]
        private WorldBounds worldBounds;
        
        [SerializeField]
        private UIInstaller uiInstaller;
        [SerializeField]
        private SnakeInstaller snakeInstaller;
        [SerializeField]
        private CoinInstaller coinInstaller;
        
        public override void InstallBindings()
        {
            Difficulty difficulty = new Difficulty(levels);

            this.Container.BindInterfacesAndSelfTo<GameCycle>().AsSingle();
            this.Container.BindInterfacesAndSelfTo<Difficulty>().FromInstance(difficulty).AsSingle();
            this.Container.BindInterfacesAndSelfTo<WorldBounds>().FromInstance(worldBounds).AsSingle();
            
            this.Container.BindInterfacesAndSelfTo<GameWinController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<ChangeDifficultyController>().AsSingle().NonLazy();
            
            
            this.Container.Install(uiInstaller);
            this.Container.Install(snakeInstaller);
            this.Container.Install(coinInstaller);
        }
    }
}

