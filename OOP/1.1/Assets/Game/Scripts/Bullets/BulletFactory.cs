using Modules.Utils;
using UnityEngine;

namespace Game
{
    public abstract class BulletFactory : ScriptableObject , IFactory<Bullet>
    {
        [field: SerializeField]
        public GameObject _bulletPrefab;
        
        [field: SerializeField]
        public TeamType _team;

        [SerializeField, Range(0, 31)] public int _layer;

        [field: SerializeField] public int _damage;

        [field: SerializeField] public float _speed;
        
        [field: SerializeField]
        public GameObject ViewVFX { get; private set; }
        
        [field: SerializeField]
        public GameObject ExplosionVFX  { get; private set; }
        
        [field: SerializeField]
        public TransformBounds _transformBounds;
        
        public abstract Bullet Create();
    }
}