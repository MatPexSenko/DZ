using UnityEngine;

namespace Game
{
    public sealed class Explosion : MonoBehaviour
    {
        private Transform _transform;
        [SerializeField]private ParticleSystem _particleSystem;

        public void Init()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _transform = GetComponent<Transform>();
        }
        public void Initialize(Transform transform)
        {
            _transform.position = transform.position;
            _particleSystem.Play();
        }
    }
}