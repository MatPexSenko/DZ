using UnityEngine;

namespace Game
{
    public class EnemyFactory : Factory<Enemy>
    {
        [SerializeField]
        private ShipConfig _shipConfig;

        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private BulletManager _bulletManager;
        public override Enemy Create()
        {
            var obj = Instantiate(_shipConfig.shipPrefab, _container);
            var enemy = obj.GetComponent<Enemy>();
            enemy.Construct(_shipConfig.Health, _shipConfig.MoveSpeed,_shipConfig.FireCooldown, _bulletManager);

            return enemy;
        }
    }
}