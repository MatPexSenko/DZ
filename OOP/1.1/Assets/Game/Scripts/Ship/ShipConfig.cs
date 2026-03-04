using UnityEngine;

namespace Game
{
    // +
    public abstract class ShipConfig : ScriptableObject
    {
        [field: SerializeField]
        public GameObject shipPrefab;
        
        [Header("Core")]
        [field: SerializeField]
        public int Health = 5;
        
        [field: SerializeField]
        public float MoveSpeed = 5;

        [field: SerializeField]
        public float FireCooldown = 0.25f;
    }
}