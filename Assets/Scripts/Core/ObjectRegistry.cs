using System.Collections.Generic;
using UnityEngine;

namespace EditorTool.Core
{
    public sealed class ObjectRegistry : MonoBehaviour
    {
        public static ObjectRegistry Instance { get; private set; }
        

        private readonly List<GameObject> registeredObjects = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public GameObject SpawnObject(GameObject prefab)
        {
            if (prefab == null)
                return null;

            GameObject instance = Instantiate(
                prefab,
                Vector3.zero,
                Quaternion.identity,
                null
            );

            Register(instance);

            SelectionManager.Instance.SetSelection(instance);

            return instance;
        }


        public void Register(GameObject instance)
        {
            if (instance == null)
                return;

            if (registeredObjects.Contains(instance))
                return;

            registeredObjects.Add(instance);
        }

        public void Unregister(GameObject instance)
        {
            if (instance == null)
                return;

            registeredObjects.Remove(instance);
        }

        public IReadOnlyList<GameObject> GetAllObjects()
        {
            return registeredObjects;
        }

        public int GetSpawnIndex(GameObject obj)
        {
            return registeredObjects.IndexOf(obj);
        }

        public void DeleteObject(GameObject target)
        {
            if (target == null)
                return;

            Unregister(target);
            Destroy(target);
        }


    }
}
