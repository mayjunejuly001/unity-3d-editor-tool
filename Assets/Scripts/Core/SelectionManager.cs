using System;
using UnityEngine;

namespace EditorTool.Core
{
    public class SelectionManager : MonoBehaviour
    {
        public static SelectionManager Instance { get; private set; }

        public event Action<GameObject> OnSelectionChanged;

        public GameObject CurrentSelection { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetSelection(GameObject target)
        {
            if (target == CurrentSelection)
                return;

            CurrentSelection = target;
            NotifySelectionChanged();
        }

        public void ClearSelection()
        {
            if (CurrentSelection == null)
                return;

            CurrentSelection = null;
            NotifySelectionChanged();
        }

        private void NotifySelectionChanged()
        {
            OnSelectionChanged?.Invoke(CurrentSelection);
        }
    }
}

