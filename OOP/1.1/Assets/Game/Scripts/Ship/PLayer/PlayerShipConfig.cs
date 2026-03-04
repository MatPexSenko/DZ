using Modules.Utils;
using UnityEngine;
namespace Game
{
    [CreateAssetMenu(menuName = "Game/PlayerShipControllerInfo", order = 0)]
    public sealed class PlayerShipConfig : ShipConfig
    {
        // public PlayerShip Create()
        // {
        //     var obj = Instantiate(shipPrefab);
        //     var ship = obj.GetComponent<PlayerShip>();
        //     ship.gameObject.layer = _layer;
        //     ship.Construct(Health, MoveSpeed, FireCooldown);
        //     ship.SetBounds(_playerBounds);
        //     return ship;
        // }
    }
}