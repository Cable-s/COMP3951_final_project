using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    private const float MaxZoomOut = 2f;
    private const float MinZoomIn = 0.7f;

    private Vector3 origin;
    private Vector3 difference;

    private Camera mainCamera;

    private bool isDragging;

    private Bounds cameraBounds;
    private Vector3 targetPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        var height = mainCamera.orthographicSize;
        var width = height * mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }

    private void Update()
    {
        var zoomChange = Input.GetAxis("Mouse ScrollWheel");
        var newZoomSize = mainCamera.orthographicSize - zoomChange;
        mainCamera.orthographicSize = Mathf.Clamp(newZoomSize, MinZoomIn, MaxZoomOut);
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            origin = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        isDragging = ctx.started || ctx.performed;
    }

    private void LateUpdate()
    {
        if (!isDragging)
            return;

        difference = GetMousePosition() - transform.position;

        targetPosition = origin - difference;
        targetPosition = GetCameraBounds();

        transform.position = targetPosition;
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
        );
    }

    private Vector3 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}