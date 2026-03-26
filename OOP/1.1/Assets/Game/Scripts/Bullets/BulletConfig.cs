using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Game/BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField]
        public GameObject BulletPrefab;

        [field: SerializeField] public int Damage;

        [field: SerializeField] public float Speed;
        
        [field: SerializeField] public TeamType Team;
        
        [field: SerializeField]
        public GameObject ViewVFX { get; private set; }
        
        [field: SerializeField]
        public GameObject ExplosionVFX  { get; private set; }
    }
}