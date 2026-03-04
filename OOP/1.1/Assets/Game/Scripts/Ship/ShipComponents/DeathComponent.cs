using UnityEngine;

namespace Game
{
    public sealed class DeathComponent : MonoBehaviour
    {
        [SerializeField] private Ship _ship;

        [SerializeField] private ExplosionManager _explosionManager;

        private void Awake()
        {
            _explosionManager = FindFirstObjectByType<ExplosionManager>();
        }
        
        private void OnEnable()
        {
            _ship.OnDead += Dead;
        }

        private void OnDisable()
        {
            _ship.OnDead -= Dead;
        }
        private void Dead()
        {
            _explosionManager.BigExplosionInit(this.transform);
            gameObject.SetActive(false);
        }
    }
}