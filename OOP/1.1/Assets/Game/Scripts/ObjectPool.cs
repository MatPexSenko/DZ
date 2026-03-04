using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public sealed class ObjectPool<T>  where T : MonoBehaviour
    {
        private readonly Stack<T> _pool = new();
        private readonly IFactory<T> factory;
        public ObjectPool(IFactory<T> factory, Transform parent)
        {
            this.factory = factory;
            for (var i = 0; i < 10; i++)
            {
                T bullet = this.factory.Create();
                bullet.transform.parent = parent;
                bullet.gameObject.SetActive(false);
                _pool.Push(bullet);
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