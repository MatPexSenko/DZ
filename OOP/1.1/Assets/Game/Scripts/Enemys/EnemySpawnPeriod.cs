using UnityEngine;

namespace Game
{
    public sealed class EnemySpawnPeriod : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField]
        private float _minSpawnCooldown = 2;

        [SerializeField]
        private float _maxSpawnCooldown = 3;

        [SerializeField]
        private EnemyManager _enemyManager;
        
        [SerializeField]
        private bool _isActive = true;
        
        private float _spawnCooldown;
        private float _spawnTime;

        
        private void ResetSpawnCooldown()
        {
            _spawnCooldown = Random.Range(_minSpawnCooldown, _maxSpawnCooldown);
            _spawnTime = Time.fixedTime;
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
            
            _enemyManager.SpawnEnemy();
            
            ResetSpawnCooldown();
        }
        
        
        public void Stop()
        {
            _isActive = false;
        }
    }
}