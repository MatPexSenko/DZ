using UnityEngine;

namespace Game
{
    [CreateAssetMenu(
        fileName = "BulletViewConfig",
        menuName = "Game/New ExplosionViewConfig"
    )]
    public sealed class ExplosionConfig : ScriptableObject , IFactory<Explosion>
    {
        [SerializeField]
        private GameObject _explosionVFX;
        public Explosion Create()
        {
            var obj = Instantiate(_explosionVFX);
            var explosion = obj.AddComponent<Explosion>();
            explosion.Init();
            return explosion;
        }
    }
}