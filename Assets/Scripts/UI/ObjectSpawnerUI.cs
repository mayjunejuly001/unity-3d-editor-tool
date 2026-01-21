using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EditorTool.Core;

namespace EditorTool.UI
{
    public sealed class ObjectSpawnerUI : MonoBehaviour
    {
        [System.Serializable]
        public struct SpawnEntry
        {
            public string displayName;
            public GameObject prefab;
            public Button button;
        }

        [SerializeField]
        private List<SpawnEntry> spawnEntries = new();

        private void Awake()
        {
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            foreach (var entry in spawnEntries)
            {
                if (entry.button == null || entry.prefab == null)
                    continue;

                entry.button.onClick.AddListener(() =>
                {
                    ObjectRegistry.Instance.SpawnObject(entry.prefab);
                });
            }
        }
    }
}
