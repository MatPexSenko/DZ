using System;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    [Serializable]
    public sealed class UIInstaller : Installer
    {
        [SerializeField]
        private GameUI gameUI;
        
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<GameUI>().FromInstance(gameUI).AsSingle().NonLazy();
            
            this.Container.BindInterfacesAndSelfTo<GameOverUIController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<DifficultyUIController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<ScoreUIController>().AsSingle().NonLazy();
        }
    }
}