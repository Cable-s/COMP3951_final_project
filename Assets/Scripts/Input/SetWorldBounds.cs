using UnityEngine;
using UnityEngine.Tilemaps;

public class SetWorldBounds : MonoBehaviour
{
    private void Awake()
    {
        //TilemapRenderer tilemap = GetComponent<TilemapRenderer>();

        //if (tilemap == null)
        //{
        //    Debug.LogError("SetWorldBounds: No Tilemap component found on this GameObject.");
        //    return;
        //}

        //Bounds bounds = tilemap.localBounds;
        //Globals.WorldBounds = bounds;

        //Debug.Log($"World bounds set to: {bounds}");

        var bounds = GetComponent<Collider2D>().bounds;
        Globals.WorldBounds = bounds;

        Debug.Log($"World bounds set to: {bounds}");
    }
}
