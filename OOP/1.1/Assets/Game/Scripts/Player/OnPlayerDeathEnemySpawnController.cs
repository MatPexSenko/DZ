using UnityEngine;
using Modules;

namespace Game
{
    public sealed class OnPlayerDeathEnemySpawnController : MonoBehaviour
    {
        [SerializeField]private Player playerPlayer;
        [SerializeField]private EnemySpawnPeriod _enemySpawnPEriod;

        private void OnEnable()
        {
            playerPlayer.HealthComponent.OnDead += _enemySpawnPEriod.Stop;
        }
        private void OnDisable()
        {
            playerPlayer.HealthComponent.OnDead -= _enemySpawnPEriod.Stop;
        }
    }
}