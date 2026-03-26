using UnityEngine;
using DG.Tweening;

namespace Game
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField]
        private GameObjectPull _gameObjectPull;
        [SerializeField]private GameObject _VFXExplosion;
        
        [Header("Config")]
        [SerializeField]
        private ShipViewConfig _viewConfig;
        
        [Header("Visual")]
        [SerializeField]
        private Renderer _renderer;
        
        [SerializeField]
        private ParticleSystem _fireVFX;

        private Material _material;
        private Tweener _damageAnimation;
        
        [SerializeField]
        private Transform _viewTransform;

        [SerializeField] 
        private Enemy _ship;
        
        [SerializeField]
        private AudioClip _fireSFX;

        [SerializeField]
        private AudioClip _damageSFX;
        
        [SerializeField]
        private AudioSource _audioSource;

        private void Awake()
        {
            _material = new Material(_viewConfig.MaterialPrefab);
            _renderer.material = _material;
            
            _gameObjectPull = GameObjectPull.Instance;
        }

        private void OnEnable()
        {
            _ship.HealthComponent.OnHealthChanged += AnimateDamage;
            _ship.FireComponent.OnFire += () => _fireVFX.Play();
            _ship.FireComponent.OnFire+= () => PlaySound(_fireSFX);
            _ship.HealthComponent.OnHealthChanged+= (_, _) => PlaySound(_damageSFX);
            _ship.HealthComponent.OnDead += DeadExplosion;
        }
        private void OnDisable()
        {
            _ship.HealthComponent.OnHealthChanged -= AnimateDamage;
            _ship.FireComponent.OnFire -= () => _fireVFX.Play();
            _ship.FireComponent.OnFire-= () => PlaySound(_fireSFX);
            _ship.HealthComponent.OnHealthChanged-= (_, _) => PlaySound(_damageSFX);
            _ship.HealthComponent.OnDead -= DeadExplosion;
        }
        private void AnimateDamage(int _, int __)
        {
            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();

            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                _viewConfig.HitDuration,
                progress => _material?.SetFloat(_viewConfig.HitPropertyName,
                    _viewConfig.HitAnimationCurve.Evaluate(progress))
            ).SetLink(_renderer.gameObject);
               
        }
        private void DeadExplosion()
        {
            var x = _gameObjectPull.Rent(_VFXExplosion);
            x.transform.position = this.transform.position;
            
        }
        private void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}