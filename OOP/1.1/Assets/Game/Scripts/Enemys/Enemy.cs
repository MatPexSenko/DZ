using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class Enemy : MonoBehaviour, IDamageable
    {
        [Header("Components")]
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private FireComponent fireComponent;

        public HealthComponent HealthComponent => healthComponent;
        public MoveComponent MoveComponent => moveComponent;
        public FireComponent FireComponent => fireComponent;
        
        
        [SerializeField]
        private Transform _target;
        private Vector2 _destination;
        
        [SerializeField]
        private float _stoppingDistance = 0.25f;
        
        private IEnemyDespawner _despawner;
        
        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        public void Construct(int health, float move, float cooldown, BulletManager bulletManager)
        {
            healthComponent.SetHealth(health);
            moveComponent.SetSpeed(move);
            fireComponent.SetCooldown(cooldown);
            fireComponent.SetBulletManager(bulletManager);
        }
        
        public void Awake()
        {
            moveComponent.AddCondition(() => healthComponent.IsAlive());
            fireComponent.AddCondition(() => healthComponent.IsAlive());
            fireComponent.AddCondition(() => _target != null);

            healthComponent.OnDead += Despawn;
        }
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        public void SetDestination(Vector2 destination)
        {
            _destination = destination;
        }
        public void TakeDamage(int damage)
        {
            healthComponent.Hit(damage);
        }
        
        private Vector2 GetDirectionToTarget()
        {
            if (_target == null)
                return Vector2.zero;
            Vector2 direction = (_target.position - transform.position).normalized;
            return direction;
        }
        private  void FixedUpdate()
        {
            Vector2 distance = _destination - (Vector2) this.transform.position;
            bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            
            var moveDirection = isNotReached ? distance.normalized : Vector2.zero;
        
            if (isNotReached)
            {
                moveComponent.Move(distance.normalized);
            }
            else
            {
                fireComponent.Fire(GetDirectionToTarget());
            }
        }
        public void Despawn()
        {
            _despawner.Despawn(this);
        }
        public void RestoreHealth()
        {
            healthComponent.RestoreHealth();
        }
    }
}