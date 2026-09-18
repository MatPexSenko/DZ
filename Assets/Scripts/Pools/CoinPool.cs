using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public sealed class CoinPool : MonoMemoryPool<Coin>
    {
        private readonly IWorldBounds _worldBounds;

        public CoinPool(IWorldBounds worldBounds) : base()
        {
            _worldBounds = worldBounds;
        }
        
        protected override void Reinitialize(Coin coin)
        {
            coin.Position = _worldBounds.GetRandomPosition();
            coin.Generate();
        }
    }
}