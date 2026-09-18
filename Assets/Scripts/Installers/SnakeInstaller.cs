using System;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    [Serializable]
    public sealed class SnakeInstaller : Installer
    {
        [SerializeField]
        private Snake snake;
        
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<Snake>().FromInstance(snake).AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<SnakeStopController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<SnakeSpeedController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<SnakeExpandController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<SnakeOverBoundsGameOverController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<SnakeSelfCollidedGameOverController>().AsSingle().NonLazy();
        }
    }
}