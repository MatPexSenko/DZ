using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ShipView : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField]
        private ShipControllerViewConfig _viewConfig;
        
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
        private Ship _ship;

        private void Awake()
        {
            _material = new Material(_viewConfig.MaterialPrefab);
            _renderer.material = _material;

            _ship.OnHit += AnimateDamage;
            _ship.OnMoved += AnimateMovement;
            _ship.OnFire += _ => _fireVFX.Play(); 
        }

        private void OnEnable()
        {
            _ship.OnHit += AnimateDamage;
            _ship.OnMoved += AnimateMovement;
            _ship.OnFire += _ => _fireVFX.Play(); 
        }
        private void OnDisable()
        {
            _ship.OnHit -= AnimateDamage;
            _ship.OnMoved -= AnimateMovement;
            _ship.OnFire -= _ => _fireVFX.Play(); 
        }
        private void AnimateDamage()
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
        private void AnimateMovement(Vector3 direction)
        {
            Vector3 shipAngles = _viewTransform.localEulerAngles;
            shipAngles.x = _viewConfig.MoveRotationAngle * direction.y;
            shipAngles.y = _viewConfig.MoveRotationAngle / 2 * direction.x * -1f;
            
            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = _viewConfig.MoveSpeed * Time.deltaTime;
            _viewTransform.localRotation = Quaternion.Lerp(_viewTransform.localRotation, shipRotation, t);
        }
    }
}