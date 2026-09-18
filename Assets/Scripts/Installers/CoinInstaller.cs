using System;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    [Serializable]
    public sealed class CoinInstaller : Installer
    {
        [SerializeField]
        private Coin coin;
        [SerializeField]
        private Transform coinContainer;
        
        public override void InstallBindings()
        {
            this.Container.BindMemoryPool<Coin,CoinPool>()
                .FromComponentInNewPrefab(coin)
                .UnderTransform(coinContainer)
                .AsSingle()
                .NonLazy();
            
            this.Container.BindInterfacesAndSelfTo<CoinSpawnController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<CoinPickUpController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<CoinManager>().AsSingle().NonLazy();
        }
    }
}