using UnityEngine;

/// <summary>
/// Sets the global world boundaries based on the Collider2D attached to the same GameObject.
/// <author>Phyo Thu Kha</author>
/// </summary>
public class SetWorldBounds : MonoBehaviour
{
    /// <summary>
    /// Called when the script instance is being loaded.
    /// Retrieves the Collider2D bounds and assigns them to the global WorldBounds variable.
    /// </summary>
    private void Awake()
    {
        // Retrieve the Collider2D component attached to this GameObject.
        var bounds = GetComponent<Collider2D>().bounds;
        // Set the global world boundaries so other scripts can use it for their logic.
        Globals.WorldBounds = bounds;
    }
}
