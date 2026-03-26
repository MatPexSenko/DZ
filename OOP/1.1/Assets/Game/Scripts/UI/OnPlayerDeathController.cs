using Modules.UI;
using UnityEngine;

namespace Game
{
    public sealed class OnPlayerDeathGameOverController : MonoBehaviour
    {
        [SerializeField]private Player playerPlayer;
        [SerializeField]private GameOverView _gameOverView;

        private void OnEnable()
        {
            playerPlayer.HealthComponent.OnDead += _gameOverView.Show;
        }
        private void OnDisable()
        {
            playerPlayer.HealthComponent.OnDead -= _gameOverView.Show;
        }
    }
}