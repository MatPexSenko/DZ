using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SnakeGame
{
    public sealed class CoinManager
    {
        public event Action<ICoin> OnCoinPicked;
        public event Action OnAllCoinPicked;
        
        private readonly CoinPool _coinPool;

        public int TotalScore => _totalScore;
        private int _totalScore;
        public Dictionary<Vector2Int, Coin> Coins => _coins;
        private Dictionary<Vector2Int, Coin> _coins = new Dictionary<Vector2Int, Coin>();

        public CoinManager(CoinPool coinPool)
        {
            this._coinPool = coinPool;
        }

        private void CheckAllCoinsPicked()
        {
            if (_coins.Count<=0)
            {
                OnAllCoinPicked?.Invoke();
            }
        }

        public void SpawnCoin()
        {
            var coin = _coinPool.Spawn();
            _coins[coin.Position] = coin;
        }

        public void DespawnCoin(Coin coin)
        {
            _totalScore += coin.Score;
            _coinPool.Despawn(coin);
            _coins.Remove(coin.Position);
            CheckAllCoinsPicked();
            OnCoinPicked?.Invoke(coin);
        }
    }
}