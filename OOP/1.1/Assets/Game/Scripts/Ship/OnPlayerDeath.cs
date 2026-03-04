using Modules.UI;
using UnityEngine;

namespace Game
{
    public sealed class OnPlayerDeath : MonoBehaviour
    {
        [SerializeField]private PlayerShip _playerShip;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField]private GameOverView _gameOverView;

        private void OnEnable()
        {
            _playerShip.OnDead += _enemyManager.Stop;
            _playerShip.OnDead += _gameOverView.Show;
        }
        private void OnDisable()
        {
            _playerShip.OnDead -= _enemyManager.Stop;
            _playerShip.OnDead -= _gameOverView.Show;
        }
    }
}