using UnityEngine;

namespace Game
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        [SerializeField]private Ship _ship;

        public void SetShip(Ship ship)
        {
            this._ship = ship;
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _ship.Fire();

            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            
            _ship.MoveAt(new Vector2(dx, dy));
        }
    }
}