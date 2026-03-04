using UnityEngine;

namespace Game
{
    public sealed class AudioComponent : MonoBehaviour
    {
        [SerializeField]
        private Ship _ship;
        
        [SerializeField]
        private AudioClip _fireSFX;

        [SerializeField]
        private AudioClip _damageSFX;
        
        [SerializeField]
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _ship.OnFire+= _ => PlaySound(_fireSFX);
            _ship.OnHealthChanged+= (_, _) => PlaySound(_damageSFX);
        }

        private void OnDisable()
        {
            _ship.OnFire-= _ => PlaySound(_fireSFX);
            _ship.OnHealthChanged-= (_, _) => PlaySound(_damageSFX);
        }

        private void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}