using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +

    public sealed class MoveComponent : MonoBehaviour
    {
        public event Action<Vector2> OnMoved, OnChangePosition;
        
        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        [SerializeField]
        private float _speed;

        private Vector2? _direction;

        public void SetSpeed(float speed) => _speed = speed;

        public void Move(Vector2 direction)
        {
            _direction = direction;
        }
        
        private readonly List<Func<bool>> _conditions = new();
        
        public void AddCondition(Func<bool> condition) => _conditions.Add(condition);
        
        protected bool CanMove()
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
        public void FixedUpdate()
        {
            if (!_direction.HasValue || !CanMove())
                return;

            Vector2 direction = _direction.Value;
            Vector2 newPosition = _rigidbody.position + direction * (_speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
            OnChangePosition?.Invoke(newPosition);
            OnMoved?.Invoke(direction);
            _direction = null;
        }
    }
}