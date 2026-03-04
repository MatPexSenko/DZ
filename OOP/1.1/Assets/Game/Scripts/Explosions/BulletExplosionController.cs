using UnityEngine;

namespace Game
{
    public class BulletExplosionController : MonoBehaviour
    {
        [SerializeField]private BulletManager _bulletManager;
        [SerializeField] private ExplosionManager _explosionManager;

        private void OnEnable()
        {
            _bulletManager.OnBulletHit += OnHit;
        }
        private void OnDisable()
        {
            _bulletManager.OnBulletHit -= OnHit;
        }

        private void OnHit(Transform transform) => _explosionManager.SmallExplosionInit(transform);
    }
}