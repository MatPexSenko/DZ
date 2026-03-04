using UnityEngine;

namespace Game
{
    public sealed class BulletView : MonoBehaviour
    {
        [SerializeField]private GameObject _VFXPlayer,_VFXEnemy;
        public void Construct(TeamType team, Vector2 direction)
        {
            SetTeamView(team);
            SetDirection(direction);
        }

        public void SetDirection(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.forward);
        }
        private void SetTeamView(TeamType team)
        {
            if (team == TeamType.Player)
            {
                _VFXPlayer.SetActive(true);
                return;
            }
            
            _VFXEnemy.SetActive(true);
        }
    }
}