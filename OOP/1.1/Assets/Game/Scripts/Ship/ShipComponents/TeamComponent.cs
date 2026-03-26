using System;
using UnityEngine;

namespace Game
{
    public class TeamComponent : MonoBehaviour
    {
        public event Action<TeamType> OnTeamChanget;
        
        [SerializeField]
        private TeamType _team;
        public TeamType Team=>_team;

        public void SetTeam(TeamType team)
        {
            if (team == _team)
                return;
            _team = team;
            OnTeamChanget?.Invoke(_team);
        }
    }
}