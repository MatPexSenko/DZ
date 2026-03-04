using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +

    [RequireComponent(typeof(MoveComponent),typeof(FireComponent),typeof(AudioComponent))]
    public abstract class Ship : MonoBehaviour, IDamageable
    {
        public event Action OnHit;
        public event Action<int, int> OnHealthChanged;
        public event Action OnDead;
        public event Action<Ship> OnFire;
        public event Action<Vector3> OnMoved;

        [Header("Health")]
        [SerializeField]protected int currentHealth;
        [SerializeField]protected int maxHealth;

        [Header("Components")]
        [SerializeField]
        protected MoveComponent moveComponent;
        [SerializeField]
        protected FireComponent fireComponent;
        
        [SerializeField]protected Vector3 moveDirection;
        public void Construct(int health, float speed, float Cooldown )
        {
            maxHealth = health;
            currentHealth = maxHealth;
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
            currentHealth-= damage;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            OnHit?.Invoke();
            if (currentHealth <= 0)
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