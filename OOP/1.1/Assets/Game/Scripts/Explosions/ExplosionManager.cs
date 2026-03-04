using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum ExplosionType
    {
        small,
        big
    }
    
    public sealed class ExplosionManager : MonoBehaviour
    {
        private ObjectPool<Explosion> _smallExplosionPool , _bigExplosionPool;

        [SerializeField]private ExplosionConfig smallExplosionConfig, bigExplosionConfig;
        
        private Dictionary<ExplosionType, ObjectPool<Explosion>> _pools;
        
        [SerializeField]private Transform _container;
        
        private WaitForSeconds _wait;
        private void Start()
        {
            _wait = new WaitForSeconds(2f);
            _smallExplosionPool = new ObjectPool<Explosion>(smallExplosionConfig, _container);
            _bigExplosionPool = new ObjectPool<Explosion>(bigExplosionConfig, _container);
            
            _pools = new Dictionary<ExplosionType, ObjectPool<Explosion>>
            {
                { ExplosionType.small, _smallExplosionPool},
                { ExplosionType.big, _bigExplosionPool }
            };
        }
        public void SmallExplosionInit(Transform transform)
        {
            StartCoroutine(Explosion(transform, ExplosionType.small));
        }
        
        public void BigExplosionInit(Transform transform)
        {
            StartCoroutine(Explosion(transform, ExplosionType.big));
        }
        private IEnumerator Explosion(Transform transform, ExplosionType type)
        {
            var x = _pools[type].GetFromPool();
            
            x.Initialize(transform);
            
            yield return _wait;
            
            _pools[type].ReturnToPool(x);
        }
    }
}