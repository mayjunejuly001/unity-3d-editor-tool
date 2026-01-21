using UnityEngine;
using EditorTool.Core;

namespace EditorTool.Visual
{
    public sealed class SelectionHighlighter : MonoBehaviour
    {
        [SerializeField]
        private Color highlightColor = Color.yellow;

        private GameObject lastSelected;
        private Color originalColor;

        private void Start()
        {
            SelectionManager.Instance.OnSelectionChanged += HandleSelectionChanged;
        }

        private void OnDestroy()
        {
            if (SelectionManager.Instance != null)
                SelectionManager.Instance.OnSelectionChanged -= HandleSelectionChanged;
        }

        private void HandleSelectionChanged(GameObject newSelection)
        {
            ClearPreviousHighlight();
            ApplyHighlight(newSelection);
        }

        private void ApplyHighlight(GameObject target)
        {
            if (target == null)
                return;

            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer == null)
                return;

            lastSelected = target;
            originalColor = renderer.material.color;
            renderer.material.color = highlightColor;
        }

        private void ClearPreviousHighlight()
        {
            if (lastSelected == null)
                return;

            Renderer renderer = lastSelected.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = originalColor;
            }

            lastSelected = null;
        }
    }
}
