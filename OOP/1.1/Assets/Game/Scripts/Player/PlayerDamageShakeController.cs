using Modules.Utils;
using UnityEngine;

namespace Game
{
    public sealed class PlayerDamageShakeController : MonoBehaviour
    {
        [SerializeField]private Player playerPlayer;
        [SerializeField]private CameraShaker _cameraShaker;

        public void OnEnable()
        {
            playerPlayer.HealthComponent.OnHealthChanged += _cameraShaker.Shake;
        }
        public void OnDisable()
        {
            playerPlayer.HealthComponent.OnHealthChanged -= _cameraShaker.Shake;
        }
    }
}