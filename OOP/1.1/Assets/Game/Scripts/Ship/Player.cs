using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +

    [RequireComponent(typeof(MoveComponent),typeof(FireComponent))]
    [RequireComponent(typeof(HealthComponent))]
    public class Player : MonoBehaviour, IDamageable
    {
        [Header("Components")]
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private FireComponent fireComponent;

        public HealthComponent HealthComponent => healthComponent;
        public MoveComponent MoveComponent => moveComponent;
        public FireComponent FireComponent => fireComponent;
        
        [SerializeField]
        private ShipConfig _config;

        public void Awake()
        {
            moveComponent.AddCondition(() => healthComponent.IsAlive());
            fireComponent.AddCondition(() => healthComponent.IsAlive());
            
            healthComponent.SetHealth(_config.Health);
            moveComponent.SetSpeed(_config.MoveSpeed);
            fireComponent.SetCooldown(_config.FireCooldown);
        }
        public void TakeDamage(int damage)
        {
            healthComponent.Hit(damage);
        }
    }
}