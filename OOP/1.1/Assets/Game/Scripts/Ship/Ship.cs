using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +

    [RequireComponent(typeof(MoveComponent),typeof(FireComponent),typeof(AudioComponent))]
    [RequireComponent(typeof(HealthComponent))]
    public abstract class Ship : MonoBehaviour, IDamageable
    {
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;
        public event Action OnDead;
        public event Action<Ship> OnFire;
        public event Action<Vector3> OnMoved;
        
        [Header("Components")]
        [SerializeField]
        protected HealthComponent healthComponent;
        [SerializeField]
        protected MoveComponent moveComponent;
        [SerializeField]
        protected FireComponent fireComponent;
        
        [SerializeField]protected Vector3 moveDirection;
        public void Construct(int health, float speed, float Cooldown )
        {
            healthComponent.Construct(health);
            moveComponent.SetSpeed(speed);
            fireComponent.SetCooldown(Cooldown);
        }
        public void MoveAt(Vector3 position)
        {
            moveComponent.MoveStep(position);
            OnMoved?.Invoke(position);
        }

        public abstract void Fire();

        public void NotifyAboutShoot()
        {
            OnFire?.Invoke(this);
        }
        public void TakeDamage(int damage)
        {
            healthComponent.Hit(damage);
            OnHealthChanged?.Invoke(healthComponent.CurrentHealth, healthComponent.MaxHealth);
            OnHit?.Invoke();
            if (!healthComponent.IsAlive())
            {
                OnDead?.Invoke();
            }
        }

        private void OnEnable()
        {
            fireComponent.OnFire += NotifyAboutShoot;
        }
        private void OnDisable()
        {
            fireComponent.OnFire -= NotifyAboutShoot;
        }
    }
}