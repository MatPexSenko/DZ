using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    public sealed class PlayerHealthViewController : MonoBehaviour
    {
        [SerializeField]private HealthView _healthView;
        [SerializeField]private Player playerPlayer;

        public void OnEnable()
        {
            playerPlayer.HealthComponent.OnHealthChanged += _healthView.SetHealth;
        }
        public void OnDisable()
        {
            playerPlayer.HealthComponent.OnHealthChanged -= _healthView.SetHealth;
        }
    }
}