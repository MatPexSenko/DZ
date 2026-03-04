using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    // +

    public sealed class EnemyManager : MonoBehaviour, IEnemyDespawner
    {
        public event Action<int> OnShipDefeated;
        
        [Header("Spawn")]
        [SerializeField]
        private float _minSpawnCooldown = 2;

        [SerializeField]
        private float _maxSpawnCooldown = 3;
        
        private float _spawnCooldown;
        private float _spawnTime;
        
        [Header("Pool")]
        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private EnemyShipConfig config;

        private ObjectPool<EnemyShip> _pool;

        [Header("Target")]
        [SerializeField]
        private Ship _player;
        
        [Header("Points")]
        [SerializeField]
        private Transform[] _spawnPositions;
        
        [SerializeField]
        private Transform[] _attackPositions;
        
        private int _spawnIndex;
        private int _attackIndex;
        
        [Header("Bullets")]
        [SerializeField]
        private BulletManager _bulletWorld;
        
        [SerializeField]private int _destroyedEnemies;
        private bool _isActive = true;
        
        private void Awake()
        {
            _pool = new ObjectPool<EnemyShip>(config, _container);
            _spawnPositions.Shuffle();
            _attackPositions.Shuffle();
        }
        
        private void Start()
        {
            this.ResetSpawnCooldown();
        }

        private void FixedUpdate()
        {
            float time = Time.fixedTime;
            if (time - _spawnTime < _spawnCooldown || !_isActive)
                return;
            
            SpawnEnemy();
            
            this.ResetSpawnCooldown();
        }

        private void SpawnEnemy()
        {
            var enemy = _pool.GetFromPool();
            enemy.transform.position = this.NextSpawnPosition();
            enemy.SetDestination(this.NextDestination());
            enemy.SetDespawner(this);
            
            enemy.RestoreHealth();
            enemy.SetTarget(_player.transform);
        }
        private void ResetSpawnCooldown()
        {
            _spawnCooldown = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            _spawnTime = Time.fixedTime;
        }

        public void Despawn(EnemyShip enemyShip)
        {
            _destroyedEnemies++;
            OnShipDefeated?.Invoke(_destroyedEnemies);
            _pool.ReturnToPool(enemyShip);
        }
        private Vector3 NextSpawnPosition()
        {
            if (_spawnIndex >= _spawnPositions.Length)
            {
                _spawnPositions.Shuffle();
                _spawnIndex = 0;
            }

            return _spawnPositions[_spawnIndex++].position;
        }

        private Vector3 NextDestination()
        {
            if (_attackIndex >= _attackPositions.Length)
            {
                _attackPositions.Shuffle();
                _attackIndex = 0;
            }

            return _attackPositions[_attackIndex++].position;
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}