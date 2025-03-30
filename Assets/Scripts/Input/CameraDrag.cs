using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// Allows the camera to be dragged around the scene and zoomed in/out.
/// The script calculates camera bounds based on a globally defined world boundary.
/// <author>Caleb Janzen</author>
/// <author>Phyo Thu Kha</author>
/// </summary>
public class CameraDrag : MonoBehaviour
{
    // Constants defining the minimum and maximum orthographic sizes (zoom levels) for the camera.
    private const float MaxZoomOut = 2f;
    private const float MinZoomIn = 0.7f;

    // The starting position of the drag in world coordinates.
    private Vector3 origin;
    // The difference between the current mouse position and the camera's position.
    private Vector3 difference;

    // Cached reference to the main camera.
    private Camera mainCamera;

    // Indicates whether the camera is currently being dragged.
    private bool isDragging;

    // The computed boundaries within which the camera is allowed to move.
    private Bounds cameraBounds;
    // The desired target position for the camera after applying drag calculations.
    private Vector3 targetPosition;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Caches the main camera reference.
    /// </summary>
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Initializes the camera bounds based on the global world bounds and the camera's size.
    /// </summary>
    private void Start()
    {
        // Calculate the half-height of the camera in world units.
        var height = mainCamera.orthographicSize;
        // Calculate the half-width based on the camera's aspect ratio.
        var width = height * mainCamera.aspect;

        // Adjust the minimum and maximum X coordinates for the camera's center.
        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        // Adjust the minimum and maximum Y coordinates for the camera's center.
        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        // Create a new Bounds object using the calculated min and max values.
        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }

    /// <summary>
    /// Called every frame to handle zooming via the mouse scroll wheel.
    /// </summary>
    private void Update()
    {
        // Get the amount of scroll (zoom change) from the mouse wheel.
        var zoomChange = Input.GetAxis("Mouse ScrollWheel");
        // Calculate the new orthographic size and clamp it between the min and max values.
        var newZoomSize = mainCamera.orthographicSize - zoomChange;
        mainCamera.orthographicSize = Mathf.Clamp(newZoomSize, MinZoomIn, MaxZoomOut);
    }

    /// <summary>
    /// Called by the input system to handle drag events.
    /// Sets the drag origin when the drag starts.
    /// </summary>
    /// <param name="ctx">The context of the input callback.</param>
    public void OnDrag(InputAction.CallbackContext ctx)
    {
        // If the drag is starting, record the initial position in world coordinates.
        if (ctx.started)
            origin = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Update the dragging flag if the drag is starting or in progress.
        isDragging = ctx.started || ctx.performed;
    }

    /// <summary>
    /// Called after all Update calls.
    /// Handles camera movement while dragging and applies boundary constraints.
    /// </summary>
    private void LateUpdate()
    {
        if (!isDragging)
            return;

        // Calculate the difference between the current mouse position (in world coordinates) and the camera's position.
        difference = GetMousePosition() - transform.position;

        // Determine the new target position based on the drag.
        targetPosition = origin - difference;
        // Clamp the target position within the defined camera bounds.
        targetPosition = GetCameraBounds();

        // Move the camera to the clamped target position.
        transform.position = targetPosition;
    }

    /// <summary>
    /// Clamps the target position within the precomputed camera bounds.
    /// </summary>
    /// <returns>A Vector3 that is clamped within the boundaries.</returns>
    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z // Keep the current Z position unchanged.
        );
    }

    /// <summary>
    /// Retrieves the current mouse position in world coordinates.
    /// </summary>
    /// <returns>The mouse position converted from screen to world coordinates.</returns>
    private Vector3 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}
