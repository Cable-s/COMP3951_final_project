using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


///This module is for the various buildings that the player can build.
///<author>Caleb Janzen</author>

/// <summary>
/// Manages building-related interactions within the Unity game environment, including adding, removing, and tracking buildings, as well as resource management.
/// Author: Caleb Janzen
/// </summary>
public class BuildingManager : MonoBehaviour
{
    /// <summary>
    /// Tile assets representing different building types. Assigned via Unity Inspector.
    /// </summary>
    [SerializeField] internal TileBase waterBuildingTile, forestTileBuilding, mountainTileBuilding, grasslandTileBuilding, farmTileBuilding, barracksTileBuilding, mineTileBuilding;

    /// <summary>
    /// Reference to the ResourceManager for managing game resources.
    /// </summary>
    [SerializeField] private ResourceManager resourceManager;

    /// <summary>
    /// Reference to the MapGenerator responsible for managing the game map.
    /// </summary>
    [SerializeField] private MapGenerator mapGenerator;

    /// <summary>
    /// Tilemap used for placing buildings.
    /// </summary>
    [SerializeField] private Tilemap buildingMap;

    /// <summary>
    /// Dictionary holding all currently placed buildings mapped by their positions.
    /// </summary>
    private Dictionary<Vector3Int, IBuilding> buildingDict;

    /// <summary>
    /// Unity built-in method called on script initialization.
    /// Initializes building tiles and building dictionary.
    /// </summary>
    public void Start()
    {
        buildingDict = new Dictionary<Vector3Int, IBuilding>();

        // Assign tiles to static fields of buildings.
        LumberMill.tile = forestTileBuilding;
        Dock.tile = waterBuildingTile;
        Quarry.tile = mountainTileBuilding;
        House.tile = grasslandTileBuilding;
        Farm.tile = farmTileBuilding;
        Barracks.tile = barracksTileBuilding;
        Mine.tile = mineTileBuilding;
    }

    /// <summary>
    /// Adds a new building at the specified position if resources are sufficient.
    /// </summary>
    /// <param name="buildingType">Type of the building to add (e.g., "Farm", "House").</param>
    /// <param name="position">Position on the Tilemap to place the building.</param>
    public void AddBuilding(string buildingType, Vector3Int position)
    {
        IBuilding currentBuilding = buildingType switch
        {
            "House" => new House(position),
            "Farm" => new Farm(position),
            "Barracks" => new Barracks(position),
            "Dock" => new Dock(position),
            "Quarry" => new Quarry(position),
            "Mine" => new Mine(position),
            "LumberMill" => new LumberMill(position),
            _ => null
        };

        if (currentBuilding != null &&
            resourceManager.peopleCount >= currentBuilding.peopleCost &&
            resourceManager.woodCount >= currentBuilding.woodCost &&
            resourceManager.metalCount >= currentBuilding.metalCost)
        {
            resourceManager.peopleCount -= currentBuilding.peopleCost;
            resourceManager.woodCount -= currentBuilding.woodCost;
            resourceManager.metalCount -= currentBuilding.metalCost;
            resourceManager.updateResourceCountText();
            AddBuildingToTile(currentBuilding);
            buildingDict.Add(position, currentBuilding);

            // Preserve water tiles when building a dock.
            if (buildingType != "Dock")
            {
                mapGenerator.Tilemap.SetTile(position, null);
            }

            mapGenerator.RevealFog(position, currentBuilding.sight);
        }
    }

    /// <summary>
    /// Removes a building at the specified position if it exists.
    /// </summary>
    /// <param name="position">Position of the building to remove.</param>
    public void RemoveBuiding(Vector3Int position)
    {
        if (buildingDict.ContainsKey(position))
        {
            IBuilding currentBuilding = buildingDict[position];
            currentBuilding.RemoveBuilding(buildingMap);
            buildingDict.Remove(position);
        }
    }

    /// <summary>
    /// Adds a building instance to the Tilemap.
    /// </summary>
    /// <param name="building">Building instance to be added.</param>
    public void AddBuildingToTile(IBuilding building)
    {
        building.AddBuildingToTile(buildingMap);
    }

    /// <summary>
    /// Iterates through all placed buildings and outputs their resource contributions.
    /// </summary>
    public void OutputResources()
    {
        foreach (IBuilding building in buildingDict.Values)
        {
            building.OutputResources(resourceManager);
        }
    }

    /// <summary>
    /// Finds and returns the position of the nearest building to the specified location using Manhattan distance.
    /// </summary>
    /// <param name="position">Starting position to search from.</param>
    /// <returns>Position of the nearest building, or null if none exist.</returns>
    public Vector3Int? GetNearestBuilding(Vector3Int position)
    {
        if (buildingDict.Count == 0)
        {
            return null;
        }

        int lowestManhattanDistance = int.MaxValue;
        Vector3Int? target = null;

        foreach (IBuilding building in buildingDict.Values)
        {
            int manhattanDistance = Mathf.Abs(building.position.x - position.x) + Mathf.Abs(building.position.y - position.y);

            if (manhattanDistance < lowestManhattanDistance)
            {
                lowestManhattanDistance = manhattanDistance;
                target = building.position;
            }
        }

        return target;
    }
}


