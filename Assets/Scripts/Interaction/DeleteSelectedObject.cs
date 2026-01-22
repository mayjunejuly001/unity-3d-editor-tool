using UnityEngine;
using EditorTool.Core;

namespace EditorTool.Interaction
{
    public sealed class DeleteSelectedObject : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Delete) &&
                !Input.GetKeyDown(KeyCode.Backspace))
                return;

            GameObject selected = SelectionManager.Instance.CurrentSelection;
            if (selected == null)
                return;

            ObjectRegistry.Instance.DeleteObject(selected);
            SelectionManager.Instance.ClearSelection();
        }
    }
}
