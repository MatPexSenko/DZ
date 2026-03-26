using UnityEngine;

namespace Game
{
    public sealed class Explosion : MonoBehaviour
    {
        [SerializeField]private ParticleSystem _particleSystem;

        public void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        public void OnEnable()
        {
            _particleSystem.Play();
        }
    }
}