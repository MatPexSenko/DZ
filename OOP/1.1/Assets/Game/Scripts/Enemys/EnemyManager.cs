using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +

    public sealed class EnemyManager : MonoBehaviour, IEnemyDespawner
    {
        public event Action<int> OnShipDefeated;
        
        [Header("Pool")]
        [SerializeField]
        private ObjectPool<Enemy> _pool;

        [Header("Target")]
        [SerializeField]
        private Transform _player;
        
        [SerializeField]private int _destroyedEnemies;

        [SerializeField] private EnemyPositions _enemyPositions;


        public void SpawnEnemy()
        {
            var enemy = _pool.GetFromPool();
            enemy.transform.position = _enemyPositions.NextSpawnPosition();
            enemy.SetDestination(_enemyPositions.NextDestination());
            enemy.SetDespawner(this);
            
            enemy.RestoreHealth();
            enemy.SetTarget(_player.transform);
        }

        public void Despawn(Enemy enemyShip)
        {
            _destroyedEnemies++;
            OnShipDefeated?.Invoke(_destroyedEnemies);
            _pool.ReturnToPool(enemyShip);
        }
    }
}