using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class EnemyShip : Ship
    {
        [Header("Enemy")]
        [SerializeField]
        private Transform _target;
        private Vector2 _destination;

        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private float _fireTime;

        private IEnemyDespawner _despawner;
        
        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        private void OnEnable() => this.OnDead += this.OnCharacterDead;
        
        private void OnDisable() => this.OnDead -= this.OnCharacterDead;
        
        private void OnCharacterDead() => _despawner.Despawn(this);

        public void Awake()
        {
            moveComponent.AddCondition(() => currentHealth > 0);
            fireComponent.AddCondition(() => currentHealth > 0);
            fireComponent.AddCondition(() => _target.position != null);
        }

        public void SetDestination(Vector2 destination)
        {
            _destination = destination;
        }
        public void SetTarget(Transform target)
        {
            this._target = target;
        }
        public Vector2 GetDirectionToTarget()
        {
            if (_target == null)
                return Vector2.zero;
            Vector2 direction = (_target.position - transform.position).normalized;
            return direction;
        }
        protected  void FixedUpdate()
        {
            Vector2 distance = _destination - (Vector2) this.transform.position;
            bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;
            
            moveDirection = isNotReached ? distance.normalized : Vector3.zero;
        
            if (isNotReached)
            {
                moveComponent.MoveStep(distance.normalized);
            }
            else
            {
                Fire();
            }
        }

        public void RestoreHealth()
        {
            currentHealth=maxHealth;
        }
        public override void Fire()
        {
            if (_target == null || !_target.gameObject.activeInHierarchy)
                return;

            var direction = GetDirectionToTarget();
            fireComponent.Fire(TeamType.Enemy, direction);
        }
    }
}