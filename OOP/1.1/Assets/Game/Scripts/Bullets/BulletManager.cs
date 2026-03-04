using System;
using System.Collections.Generic;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    
    public sealed class BulletManager : MonoBehaviour
    {
        public event Action<Transform> OnBulletHit;
        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private PlayerBulletConfig playerConfig;
        
        [SerializeField]
        private EnemyBulletConfig enemyConfig;

        private Dictionary<TeamType, ObjectPool<Bullet>> _pools;
        
        private ObjectPool<Bullet> _playerBulletPool, _enemyBulletPool;

        [SerializeField]
        private TransformBounds _levelBounds;
        private void Awake()
        {
            FillDictionary();
        }

        private void FillDictionary()
        {
            _pools = new Dictionary<TeamType, ObjectPool<Bullet>>
            {
                { TeamType.Player, _playerBulletPool = new ObjectPool<Bullet>(playerConfig, _container) },
                { TeamType.Enemy, _enemyBulletPool = new ObjectPool<Bullet>(enemyConfig, _container) }
            };
        }
        public void Spawn(Vector2 position, TeamType team)
        {
            var bullet = _pools[team].GetFromPool();
            bullet.Fire(position);
            SubscribeBullet(bullet);
        }
        public void Spawn(Vector2 position, Vector2 direction , TeamType team)
        {
            var bullet = _pools[team].GetFromPool();
            bullet.Fire(position, direction);
            SubscribeBullet(bullet);
        }

        private void  SubscribeBullet(Bullet bullet)
        {
            bullet.OnTriggerEntered += OnBulletTriggerEntered;
            bullet.OnBoundsExited += OnBoundsExited;
        }
        private void  UnSubscribeBullet(Bullet bullet)
        {
            bullet.OnTriggerEntered -= OnBulletTriggerEntered;
            bullet.OnBoundsExited -= OnBoundsExited;
        }
         private void OnBulletTriggerEntered(Bullet bullet, Transform transform)
         {
             UnSubscribeBullet(bullet);
            _pools[bullet.Team].ReturnToPool(bullet);
            OnBulletHit?.Invoke(transform);
        }

         private void OnBoundsExited(Bullet bullet)
         {
             UnSubscribeBullet(bullet);
             _pools[bullet.Team].ReturnToPool(bullet);
         }
    }
}