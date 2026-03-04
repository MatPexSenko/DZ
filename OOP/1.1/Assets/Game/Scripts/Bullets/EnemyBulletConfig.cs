using Modules.Utils;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(
        fileName = "BulletViewConfig",
        menuName = "Game/New EnemyBulletViewConfig"
    )]
    public sealed class EnemyBulletConfig : BulletFactory
    {
        public override Bullet Create()
        {
            var obj = Instantiate(_bulletPrefab);
            var bullet = obj.GetComponent<Bullet>();
            bullet.gameObject.layer = _layer;
            bullet.Construct(_damage, _speed, Vector2.zero, _team, _transformBounds);
            return bullet;
        }
    }
}