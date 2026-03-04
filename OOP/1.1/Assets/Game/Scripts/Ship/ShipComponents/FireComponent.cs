using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public sealed class FireComponent : MonoBehaviour
    {
        public event Action OnFire;
        [Header("Combat")]
        [SerializeField]private BulletManager _bulletManager;
        [SerializeField]protected Transform firePoint;
        [SerializeField]protected float _fireTime;
        [SerializeField]protected float _fireCooldown;

        private void Awake()
        {
            AddCondition(() => Time.time - _fireTime >= _fireCooldown);
            _bulletManager = FindFirstObjectByType<BulletManager>();
        }
        public void SetCooldown(float cooldown)
        {
            _fireCooldown = cooldown;
        }
        public void Fire(TeamType team)
        {
            if (CanDo(_conditions))
            {
                _bulletManager.Spawn(firePoint.position,
                    team);
                _fireTime = Time.time;
                OnFire?.Invoke();
            }
        }
        
        public void Fire(TeamType team, Vector2 direction)
        {
            if(direction == Vector2.zero)
                return;
            
            if (CanDo(_conditions))
            {
                
                _bulletManager.Spawn(firePoint.position,
                    direction,
                    team);
                _fireTime = Time.time;
                OnFire?.Invoke();
            }
        }
        private readonly List<Func<bool>> _conditions = new();
        
        public void AddCondition(Func<bool> condition) => _conditions.Add(condition);
        
        protected bool CanDo(List<Func<bool>> conditions)
        {
            foreach (var Condition in conditions)
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