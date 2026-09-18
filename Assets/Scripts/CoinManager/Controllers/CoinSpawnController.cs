using System;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class CoinSpawnController : IInitializable, IDisposable
    {
        private readonly CoinManager _coinManager;
        private readonly IDifficulty _difficulty;

        public CoinSpawnController(CoinManager coinManager, IDifficulty difficulty, IWorldBounds worldBounds)
        {
            this._coinManager = coinManager;
            this._difficulty = difficulty;
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged += SpawnCoin;
        }
        public void Dispose()
        {
            _difficulty.OnStateChanged -= SpawnCoin;
        }
        
        private void SpawnCoin()
        {
            for (int i = 0; i < _difficulty.Current; i++)
            {
                _coinManager.SpawnCoin();
            }
        }
    }
}