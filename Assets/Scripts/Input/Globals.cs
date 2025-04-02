using UnityEngine;

/// <summary>
/// A static class to hold global variables shared across scripts.
/// <author>Phyo Thu Kha</author>
/// </summary>
public static class Globals
{
    // The boundaries of the world, which can be set by another script (e.g., SetWorldBounds).
    public static Bounds WorldBounds;
}
