using System;
using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerShip : Ship
    {
        [SerializeField]
        private PlayerShipConfig _shipConfig;
        
        [SerializeField]
        private TransformBounds _playerArea;

        [SerializeField]
        private CameraShaker _cameraShaker;
        // [Header("UI")]
        // [SerializeField]
        // private GameOverView _gameOverView;
        //
        // [SerializeField]
        // private HealthView _healthView;
        private void Start()
        {
            if (_shipConfig == null)
                throw new NullReferenceException();
            
            Construct(_shipConfig.Health, _shipConfig.MoveSpeed, _shipConfig.FireCooldown);
            
            moveComponent.AddCondition(() => currentHealth > 0);
            fireComponent.AddCondition(() => currentHealth > 0);
        }
        protected void LateUpdate()
        {
            //base.LateUpdate();
            this.transform.position = _playerArea.ClampInBounds(this.transform.position);
        }

        public override void Fire()
        {
            fireComponent.Fire(TeamType.Player);
            NotifyAboutShoot();
        }
    }
}