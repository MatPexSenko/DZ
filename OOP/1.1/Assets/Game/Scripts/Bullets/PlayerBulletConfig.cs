using UnityEngine;

namespace Game
{
    // +
    [CreateAssetMenu(
        fileName = "BulletViewConfig",
        menuName = "Game/New PlayerBulletConfig"
    )]
    public sealed class PlayerBulletConfig : BulletFactory
    {
        [field: SerializeField]
        public Vector2 _direction;
        public override Bullet Create()
        {
            var obj = Instantiate(_bulletPrefab);
            var bullet = obj.GetComponent<Bullet>();
            bullet.gameObject.layer = _layer;
            bullet.Construct(_damage, _speed, _direction, _team, _transformBounds);
            return bullet;
        }
    }
}