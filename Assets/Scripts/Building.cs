using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

///This module is for the various buildings that the player can build.
///<author>Caleb Janzen</author>

///<summary>
/// The building context that interacts with Unity.
///</summary>
public class buildingContext : MonoBehaviour
{
    internal IBuilding building;
    [SerializeField] internal TileBase waterBuildingTile, forestTileBuilding, mountainTileBuilding, grasslandTileBuilding;
       
    /// <summary>
    /// unity builtin Start method
    /// </summary>
    public void Start()
    {
        //each building static TileBase must be added here beacuse they are not MonoBehaviour and cannot have [SerializeField]s
        ForestBuilding.tile = forestTileBuilding;
        WaterBuilding.tile = waterBuildingTile;
        MountainBuilding.tile = mountainTileBuilding;
        GrasslandBuilding.tile = grasslandTileBuilding;
    }

    /// <summary>
    /// set the current building for the context.
    /// </summary>
    /// <param name="newBuilding">An IBuilding</param>
    public void setBuilding(IBuilding newBuilding)
    {
        this.building = newBuilding;
    }

    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="tilePosition">The position of the building</param>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        this.building.addBuildingToTile(tilePosition, buildingMap);
    }
}

/// <summary>
/// The IBuilding interface.
/// </summary>
public interface IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile
    {
        get;
        set;
    }

    /// <summary>
    /// int property for the building cost.
    /// </summary>
    public int cost
    {
        get;
        set;
    }

    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="tilePosition">The position of the building</param>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap);
}

/// <summary>
/// The building that get built on a forest.
/// </summary>
public class ForestBuilding : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// int property for the building cost.
    /// </summary>
    public int cost { get; set; } = 5;

    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="tilePosition">The position of the building</param>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile);
    }
}

/// <summary>
/// The building that gets built on water.
/// </summary>
public class WaterBuilding : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// int property for the building cost.
    /// </summary>
    public int cost { get; set; } = 4;


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="tilePosition">The position of the building</param>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile);
    }
}

/// <summary>
/// The building that get built on mountains.
/// </summary>
public class MountainBuilding : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// int property for the building cost.
    /// </summary>
    public int cost { get; set; } = 3;


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="tilePosition">The position of the building</param>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile) ;
    }
}

/// <summary>
/// The building that gets built on grasslands.
/// </summary>
public class GrasslandBuilding : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// int property for the building cost.
    /// </summary>
    public int cost { get; set; } = 2;


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="tilePosition">The position of the building</param>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile);
    }
}
