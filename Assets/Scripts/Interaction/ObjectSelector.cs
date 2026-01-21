using UnityEngine;
using UnityEngine.EventSystems;
using EditorTool.Core;

namespace EditorTool.Interaction
{
    public sealed class ObjectSelector : MonoBehaviour
    {
        [SerializeField]
        private Camera editorCamera;

        [SerializeField]
        private LayerMask selectableLayer;

        private void Awake()
        {
            if (editorCamera == null)
                editorCamera = Camera.main;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            TrySelectObject();
        }

        private void TrySelectObject()
        {
            Ray ray = editorCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(
                ray,
                Mathf.Infinity,
                selectableLayer
            );

            if (hits.Length == 0)
            {
                SelectionManager.Instance.ClearSelection();
                return;
            }

            // Find the closest hit to the camera
            RaycastHit closestHit = hits[0];

            for (int i = 1; i < hits.Length; i++)
            {
                if (hits[i].distance < closestHit.distance)
                {
                    closestHit = hits[i];
                }
            }

            SelectionManager.Instance.SetSelection(closestHit.collider.gameObject);
        }


    }
}
