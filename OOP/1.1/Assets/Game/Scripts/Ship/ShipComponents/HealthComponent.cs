using UnityEngine;
using System;

namespace Game
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action<int, int> OnHealthChanged;
        public event Action OnDead;
        [Header("Health")]
        public int CurrentHealth { get; protected set; }

        [field: SerializeField] 
        public int MaxHealth { get; protected set; }

        public void SetHealth(int maxHealth)
        {
            this.MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }

        public void RestoreHealth()
        {
            CurrentHealth = MaxHealth;
        }

        public void Hit(int value)
        {
            CurrentHealth -= value;
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

            if (CurrentHealth <= 0)
            {
                OnDead?.Invoke();
            }
        }
    }
}