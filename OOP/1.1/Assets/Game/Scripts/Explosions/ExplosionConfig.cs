using UnityEngine;

namespace Game
{
    [CreateAssetMenu(
        fileName = "BulletViewConfig",
        menuName = "Game/New ExplosionViewConfig"
    )]
    public sealed class ExplosionConfig : ScriptableObject
    {
        [SerializeField]
        private GameObject _explosionVFX;
    }
}