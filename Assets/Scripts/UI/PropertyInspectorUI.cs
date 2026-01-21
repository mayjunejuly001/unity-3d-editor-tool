using UnityEngine;
using TMPro;
using EditorTool.Core;

namespace EditorTool.UI
{
    public sealed class PropertyInspectorUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField xInput;
        [SerializeField] private TMP_InputField yInput;
        [SerializeField] private TMP_InputField zInput;
        [SerializeField] private UnityEngine.UI.Button deleteButton;


        private GameObject currentTarget;
        private bool suppressCallbacks;

        private void Start()
        {
            SelectionManager.Instance.OnSelectionChanged += HandleSelectionChanged;

            xInput.onEndEdit.AddListener(OnXChanged);
            yInput.onEndEdit.AddListener(OnYChanged);
            zInput.onEndEdit.AddListener(OnZChanged);

            SetFieldsInteractable(false);

            deleteButton.onClick.AddListener(OnDeleteClicked);
            deleteButton.interactable = false;

        }

        private void OnDestroy()
        {
            if (SelectionManager.Instance != null)
                SelectionManager.Instance.OnSelectionChanged -= HandleSelectionChanged;
        }

        private void HandleSelectionChanged(GameObject selected)
        {
            currentTarget = selected;

            if (currentTarget == null)
            {
                ClearFields();
                SetFieldsInteractable(false);
                deleteButton.interactable = false;
                return;
            }

            UpdateFieldsFromTransform();
            SetFieldsInteractable(true);
            deleteButton.interactable = true;
        }


        private void UpdateFieldsFromTransform()
        {
            suppressCallbacks = true;

            Vector3 pos = currentTarget.transform.position;
            xInput.text = pos.x.ToString("F2");
            yInput.text = pos.y.ToString("F2");
            zInput.text = pos.z.ToString("F2");

            suppressCallbacks = false;
        }

        private void OnXChanged(string value) => ApplyPositionChange(axis: 0, value);
        private void OnYChanged(string value) => ApplyPositionChange(axis: 1, value);
        private void OnZChanged(string value) => ApplyPositionChange(axis: 2, value);

        private void ApplyPositionChange(int axis, string value)
        {
            if (suppressCallbacks || currentTarget == null)
                return;

            if (!float.TryParse(value, out float parsed))
                return;

            Vector3 pos = currentTarget.transform.position;

            if (axis == 0) pos.x = parsed;
            if (axis == 1) pos.y = parsed;
            if (axis == 2) pos.z = parsed;

            currentTarget.transform.position = pos;
            UpdateFieldsFromTransform();
        }

        private void ClearFields()
        {
            suppressCallbacks = true;
            xInput.text = string.Empty;
            yInput.text = string.Empty;
            zInput.text = string.Empty;
            suppressCallbacks = false;
        }

        private void SetFieldsInteractable(bool state)
        {
            xInput.interactable = state;
            yInput.interactable = state;
            zInput.interactable = state;
        }

        private void OnDeleteClicked()
        {
            if (currentTarget == null)
                return;

            ObjectRegistry.Instance.DeleteObject(currentTarget);
            SelectionManager.Instance.ClearSelection();
        }

    }
}
