using UnityEngine;
using EditorTool.Core;



    public sealed class EditorCameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera editorCamera;

        [Header("Movement")]
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float rotateSpeed = 3f;
        [SerializeField] private float panSpeed = 0.5f;

        [Header("Limits")]
        [SerializeField] private float minZoom = 2f;
        [SerializeField] private float maxZoom = 30f;

        private Vector3 lastMousePosition;

        private void Awake()
        {
            if (editorCamera == null)
                editorCamera = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            HandleZoom();
            HandleRotate();
            HandlePan();
            HandleFocus();
        }

        private void HandleZoom()
        {
            float scroll = Input.mouseScrollDelta.y;
            if (Mathf.Approximately(scroll, 0f))
                return;

            Vector3 localPos = editorCamera.transform.localPosition;
            localPos += Vector3.forward * scroll * zoomSpeed;
            localPos.z = Mathf.Clamp(localPos.z, -maxZoom, -minZoom);
            editorCamera.transform.localPosition = localPos;
        }

        private void HandleRotate()
        {
            if (!Input.GetMouseButton(1))
                return;

            Vector3 delta = Input.mousePosition - lastMousePosition;

            float yaw = delta.x * rotateSpeed * Time.deltaTime;
            float pitch = -delta.y * rotateSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, yaw, Space.World);
            transform.Rotate(Vector3.right, pitch, Space.Self);

        float currentX = transform.eulerAngles.x;
        if (currentX > 180f) currentX -= 360f;
        currentX = Mathf.Clamp(currentX, -80f, 80f);
        transform.rotation = Quaternion.Euler(currentX, transform.eulerAngles.y, 0f);

    }

    private void HandlePan()
        {
            if (!Input.GetMouseButton(2))
                return;

            Vector3 delta = Input.mousePosition - lastMousePosition;

            Vector3 right = editorCamera.transform.right;
            Vector3 up = editorCamera.transform.up;

            Vector3 move = (-right * delta.x + -up * delta.y) * panSpeed * Time.deltaTime;
            transform.position += move;
        }

        private void HandleFocus()
        {
            if (!Input.GetKeyDown(KeyCode.L))
                return;

            GameObject target = SelectionManager.Instance.CurrentSelection;
            if (target == null)
                return;

            transform.position = target.transform.position;
        }

        private void LateUpdate()
        {
            lastMousePosition = Input.mousePosition;
        }
    }

