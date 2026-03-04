using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public sealed class PlayerHealthController : MonoBehaviour
    {
        [SerializeField]private HealthView _healthView;
        [SerializeField]private PlayerShip _playerShip;
        [SerializeField]private CameraShaker _cameraShaker;

        public void OnEnable()
        {
            _playerShip.OnHealthChanged += _healthView.SetHealth;
            _playerShip.OnHealthChanged += _cameraShaker.Shake;
        }
        public void OnDisable()
        {
            _playerShip.OnHealthChanged -= _healthView.SetHealth;
            _playerShip.OnHealthChanged -= _cameraShaker.Shake;
        }
    }
}