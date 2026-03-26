using Modules.Utils;
using UnityEngine;

namespace Game
{
    public sealed class PlayerInBoundsController : MonoBehaviour
    {
        [SerializeField]private Player playerPlayer;
        [SerializeField]private TransformBounds _playerBounds;
        
        private void OnEnable()
        {
            playerPlayer.MoveComponent.OnChangePosition +=CorrectOnChangePosition;
        }

        public void CorrectOnChangePosition(Vector2 position)
        {
            playerPlayer.gameObject.transform.position = _playerBounds.ClampInBounds(position);
        }
        private void OnDisable()
        {
            playerPlayer.MoveComponent.OnChangePosition -= CorrectOnChangePosition;
        }
    }
}