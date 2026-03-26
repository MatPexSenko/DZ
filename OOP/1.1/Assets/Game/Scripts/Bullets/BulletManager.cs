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
        private ObjectPool<Bullet> _bulletPool;
        
        public void Spawn(Vector2 position,int damage,float speed,TeamType team)
        {
            var bullet = _bulletPool.GetFromPool();
            bullet.Construct(damage, speed, position, new Vector2(0,1), team);
            SubscribeBullet(bullet);
        }
        public void Spawn(Vector2 position,int damage,float speed, Vector2 direction , TeamType team)
        {
            var bullet = _bulletPool.GetFromPool();
            bullet.Construct(damage, speed, position, direction, team);
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
            _bulletPool.ReturnToPool(bullet);
            OnBulletHit?.Invoke(transform);
        }

         private void OnBoundsExited(Bullet bullet)
         {
             UnSubscribeBullet(bullet);
             _bulletPool.ReturnToPool(bullet);
         }
    }
}