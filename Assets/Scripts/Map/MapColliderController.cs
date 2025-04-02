using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the BoxCollider2D on a map object by disabling it when the pointer is over a UI element
/// and re-enabling it when the pointer is not over UI. This ensures that UI interactions are not blocked
/// by the map's collider.
/// <author>Phyo Thu Kha</author>
/// </summary>
public class MapColliderController : MonoBehaviour
{
    private BoxCollider2D boxCol;

    /// <summary>
    /// Called before the first frame update.
    /// Retrieves and caches the BoxCollider2D component attached to this GameObject.
    /// </summary>
    private void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Called once per frame.
    /// Checks if the pointer is currently over a UI element and disables or re-enables the collider accordingly.
    /// </summary>
    private void Update()
    {
        // If the pointer is over a UI element, disable the collider to allow UI clicks 
        // to pass through
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (boxCol.enabled)
                boxCol.enabled = false;
        }
        else
        {
            // re-enable the colldier when the UI is not active
            if (!boxCol.enabled)
                boxCol.enabled = true;
        }
    }
}
