using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public sealed class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Stack<T> _pool = new();
        
        [SerializeField]
        private Factory<T> factory;
        public void Awake()
        {
            for (var i = 0; i < 10; i++)
            {
                T obj = this.factory.Create();
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }
        }

        public T GetFromPool()
        {
            if (_pool.Count >0 )
            {
                var obj = _pool.Pop();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                return Create();
            } 
        }

        public void ReturnToPool(T data)
        {
            data.gameObject.SetActive(false);
            _pool.Push(data);
        }
    
        private T Create()
        {
            T obj = factory.Create();
            return obj;
        }
    }
}