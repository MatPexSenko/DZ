using System;
using UnityEngine;

namespace Game
{
    public sealed class BulletView : MonoBehaviour
    {
        [SerializeField]private GameObject _VFXPlayer,_VFXEnemy, _VFXExplosion;
        
        [SerializeField]private int _playerLayerMask, _enemyLayerMask;
        
        [SerializeField]private Bullet _bulletModel;
        [SerializeField] private GameObjectPull _gameObjectPull;
        private void OnHit(Bullet _,Transform transform)
        { 
            var x = _gameObjectPull.Rent(_VFXExplosion);
            x.transform.position = this.transform.position;
        }

        private void Awake()
        {
            _gameObjectPull = GameObjectPull.Instance;
        }

        private void SetDirection(Vector2 dir)
        {
            this.transform.rotation = Quaternion.LookRotation(dir);
        }

        private void SetTeamView(TeamType team)
        {
            if (team == TeamType.Player)
            {
                _VFXPlayer.SetActive(true);
                gameObject.layer = _playerLayerMask;
                _VFXEnemy.SetActive(false);
                return;
            }
            
            _VFXEnemy.SetActive(true);
            gameObject.layer = _enemyLayerMask;
            _VFXPlayer.SetActive(false);
        }
        public void OnEnable()
        {
            _bulletModel.OnTeamChanged += SetTeamView;
            _bulletModel.OnDirectionChanged += SetDirection;
            _bulletModel.OnTriggerEntered+=OnHit;
        }
        public void OnDisable()
        {
            _bulletModel.OnTeamChanged -= SetTeamView;
            _bulletModel.OnDirectionChanged -= SetDirection;
            _bulletModel.OnTriggerEntered-=OnHit;
        }
    }
}