using UnityEngine;

namespace Game
{
    public class HealthComponent : MonoBehaviour
    {
        [Header("Health")]
        public int CurrentHealth { get; protected set; }

        [field: SerializeField] 
        public int MaxHealth { get; protected set; }

        public void Construct(int maxHealth)
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
        }
    }
}