using System;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnBoundsExited;
        public event Action<Bullet,Transform> OnTriggerEntered;
        
        [SerializeField]private BulletView _view;

        [SerializeField]private TeamType _team;
        public TeamType Team => _team;
        [SerializeField]private Vector2 _direction;

        [SerializeField]private int _damage;
        [SerializeField]private float _speed;
        
        [SerializeField]private TransformBounds _bounds;

        public void Construct(int damage,float speed, Vector2 direction, TeamType team, TransformBounds bounds)
        {
            this._damage = damage;
            this._speed = speed;
            this._direction = direction;
            this._team = team;
            _bounds = bounds;
            
            _view.Construct(team, direction);
        }

        public void Fire(Vector2 position)
        {
            this.transform.position = position;
        }
        public void Fire(Vector2 position, Vector2 direction)
        {
            this.transform.position = position;
            this._direction = direction;
            _view.SetDirection(_direction);
            
        }
        private void FixedUpdate()
        {
            if (!_bounds.InBounds(this.transform.position))
            {
                OnBoundsExited?.Invoke(this);
                return;
            }
            Vector3 moveStep = _direction * _speed * Time.fixedDeltaTime;
            transform.position += moveStep;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other);
            if (!other.TryGetComponent(out IDamageable ship))
                return;
            
            ship.TakeDamage(_damage);
            
            this.OnTriggerEntered?.Invoke(this,this.transform);
        }
    }
}