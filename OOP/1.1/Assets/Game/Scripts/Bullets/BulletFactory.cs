using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class BulletFactory : Factory<Bullet>
    {
        [SerializeField]
        private TransformBounds _levelBounds;
        
        [SerializeField]
        private BulletConfig _bulletConfig;
        
        [SerializeField]
        private BulletManager _BulletManager;
        
        [SerializeField]
        private GameObjectPull _gameObjectPull;

		[SerializeField]
        private Transform _container;
        public override Bullet Create()
        {
            var obj = Instantiate(_bulletConfig.BulletPrefab, _container);
            
            var bullet = obj.GetComponent<Bullet>();
            
            bullet.SetBounds(_levelBounds);
            
            return bullet;
        }
    }
}