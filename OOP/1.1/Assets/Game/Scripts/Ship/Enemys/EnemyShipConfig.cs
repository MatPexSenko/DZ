using UnityEngine;
namespace Game
{
    
    [CreateAssetMenu(menuName = "Game/EnemyShipControllerInfo", order = 0)]
    public class EnemyShipConfig : ShipConfig, IFactory<EnemyShip>
    {
        public EnemyShip Create()
        {
            var obj = Instantiate(shipPrefab);
            var ship = obj.GetComponent<EnemyShip>();
            ship.Construct(Health, MoveSpeed, FireCooldown);
            return ship;
        }
    }
}