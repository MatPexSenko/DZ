using UnityEngine;

namespace Game
{
    public abstract class Factory<T> : MonoBehaviour
    {
        public abstract T Create();
    }
}