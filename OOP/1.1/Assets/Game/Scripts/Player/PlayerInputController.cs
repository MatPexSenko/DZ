using UnityEngine;

namespace Game
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        [SerializeField]private Player player;
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                player.FireComponent.Fire();

            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            
            player.MoveComponent.Move(new Vector2(dx, dy));
        }
    }
}