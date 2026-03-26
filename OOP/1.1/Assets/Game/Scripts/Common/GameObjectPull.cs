using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class GameObjectPull : MonoBehaviour
    {
        private Dictionary<GameObject, Stack<GameObject>> _pool = new();
        
        [SerializeField]
        private Transform _container;
        public static GameObjectPull Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        public GameObject Rent(GameObject prefab)
        {
            var id = prefab;
            
            if (!_pool.ContainsKey(id))
            {
                _pool[id] = new Stack<GameObject>();
            }
            
            if (_pool[id].Count >0 )
            {
                var obj = _pool[id].Pop();
                obj.gameObject.SetActive(true);
                StartCoroutine(ReturnToPool(id,obj));
                return obj;
            }
            else
            {
                var x = Create(prefab);
                StartCoroutine(ReturnToPool(id,x));
                return x;
            } 
        }
        
        private IEnumerator ReturnToPool(GameObject prefab, GameObject obj)
        {
            yield return new WaitForSeconds(1f);
            obj.SetActive(false);
            _pool[prefab].Push(obj);
        }
        private GameObject Create(GameObject prefab)
        {
            GameObject obj = Instantiate(prefab, _container);
            return obj;
        }
    }
}