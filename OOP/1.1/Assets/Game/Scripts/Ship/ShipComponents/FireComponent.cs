using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TeamComponent))]
    public sealed class FireComponent : MonoBehaviour
    {
        public event Action OnFire;
        
        [Header("Combat")]
        [SerializeField]private BulletManager _bulletManager;
        [SerializeField]private BulletConfig _bulletConfig;
        
        [SerializeField]protected Transform firePoint;
        [SerializeField]protected float _fireTime;
        [SerializeField]protected float _fireCooldown;
        
        [SerializeField]private TeamComponent _teamComponent;

        private void Awake()
        {
            AddCondition(() => Time.time - _fireTime >= _fireCooldown);
            _bulletManager = FindFirstObjectByType<BulletManager>();
        }

        public void SetBulletManager(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }
        public void SetCooldown(float cooldown)
        {
            _fireCooldown = cooldown;
        }
        public void Fire()
        {
            if (CanFire())
            {
                _bulletManager.Spawn(firePoint.position,
                    _bulletConfig.Damage,
                    _bulletConfig.Speed,
                    _teamComponent.Team
                    );
                _fireTime = Time.time;
                OnFire?.Invoke();
            }
        }
        
        public void Fire(Vector2 direction)
        {
            if(direction == Vector2.zero)
                return;
            
            if (CanFire())
            {
                _bulletManager.Spawn(firePoint.position,
                    _bulletConfig.Damage,
                    _bulletConfig.Speed,
                    direction,
                    _bulletConfig.Team);
                _fireTime = Time.time;
                OnFire?.Invoke();
            }
        }
        private readonly List<Func<bool>> _conditions = new();
        
        public void AddCondition(Func<bool> condition) => _conditions.Add(condition);
        
        protected bool CanFire()
        {
            foreach (var Condition in _conditions)
            {
                if (!Condition())
                {
                    return false;
                }
            }
            return true;
        }
    }
}