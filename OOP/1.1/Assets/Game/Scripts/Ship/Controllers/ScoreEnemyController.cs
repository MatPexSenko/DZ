using Modules.UI;
using UnityEngine;

namespace Game
{
    public sealed class ScoreEnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private ScoreView _scoreView;

        private void OnEnable()
        {
            _enemyManager.OnShipDefeated+=_scoreView.SetValue;
        }
        private void OnDisable()
        {
            _enemyManager.OnShipDefeated-=_scoreView.SetValue;
        }
    }
}