using System;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

///This module is for the various buildings that the player can build.
///<author>Caleb Janzen</author>

///<summary>
/// The building context that interacts with Unity.
///</summary>
public class BuildingManager : MonoBehaviour
{
    [SerializeField] internal TileBase waterBuildingTile, forestTileBuilding, mountainTileBuilding, grasslandTileBuilding, farmTileBuilding, barracksTileBuilding;
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Tilemap buildingMap;

    private Dictionary<Vector3Int, IBuilding> buildingDict;

    /// <summary>
    /// unity builtin Start method
    /// </summary>
    public void Start()
    {
        buildingDict = new Dictionary<Vector3Int,IBuilding>();

        //each building static TileBase must be added here beacuse they are not MonoBehaviour and cannot have [SerializeField]s
        LumberMill.tile = forestTileBuilding;
        Dock.tile = waterBuildingTile;
        Quarry.tile = mountainTileBuilding;
        House.tile = grasslandTileBuilding;
        Farm.tile = farmTileBuilding;
        Barracks.tile = barracksTileBuilding;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildingType"></param> The type of building to add as a string ie. "Farm"
    /// <param name="position"></param> The position of the building on the buildingMap as a Vector3Int
    public void AddBuilding(string buildingType, Vector3Int position)
    {
        IBuilding currentBuilding = buildingType switch
        {
            "House" => new House(position),
            "Farm" => new Farm(position),
            "Barracks" => new Barracks(position),
            "Dock" => new Dock(position),
            "Quarry" => new Quarry(position),
            "LumberMill" => new LumberMill(position),
            _ => null
        };

        if (resourceManager.peopleCount >= currentBuilding.peopleCost &&
            resourceManager.woodCount >= currentBuilding.woodCost &&
            resourceManager.metalCount >= currentBuilding.metalCost)
        {
            resourceManager.peopleCount -= currentBuilding.peopleCost;
            resourceManager.woodCount -= currentBuilding.woodCost;
            resourceManager.metalCount -= currentBuilding.metalCost;
            resourceManager.updateResourceCountText();
            AddBuildingToTile(currentBuilding);
            buildingDict.Add(position, currentBuilding);

            // Do not remove water tile when placing building on top
            if (buildingType != "Dock")
            {
                mapGenerator.Tilemap.SetTile(position, null);
            }

            mapGenerator.RevealFog(position, currentBuilding.sight); // reveal a small area of fog after a building is bought
        }
    }

    /// <summary>
    /// Checks if a specific tile on the buidingMap contains a building. If it does, removes the building object from the dictionary of buildings and from the map.
    /// </summary>
    /// <param name="position"></param>
    public void RemoveBuiding(Vector3Int position)
    {
        if(buildingDict.ContainsKey(position))
        {
            IBuilding currentBuilding = buildingDict[position];
            currentBuilding.RemoveBuilding(buildingMap);
            buildingDict.Remove(position);
        }
    }

    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="tilePosition">The position of the building</param>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(IBuilding building)
    {
        building.AddBuildingToTile(buildingMap);
    }

    /// <summary>
    /// Iterates through the dictionary of building objects and adds the appropriate resources for each to the resource manager
    /// </summary>
    public void OutputResources()
    {
        foreach(IBuilding building in buildingDict.Values)
        {
            building.OutputResources(resourceManager);
        }
    }

    public Vector3Int? GetNearestBuilding(Vector3Int position)
    {
        if(buildingDict.Count == 0)
        {
            return null;
        }

        int lowestManhattanDistance = int.MaxValue;
        int manhattanDistance;
        Vector3Int? target = null;

        foreach (IBuilding building in buildingDict.Values)
        {
            // Calculate the absolute difference between the two positions
            Vector3Int difference = building.position - position;

            // Get the absolute value of the difference in each direction
            Vector3Int absoluteDifference = new Vector3Int(
                Mathf.Abs(difference.x),
                Mathf.Abs(difference.y),
                0
            );

            manhattanDistance = Mathf.Abs(difference.x) + Mathf.Abs(difference.y);

            if ( manhattanDistance < lowestManhattanDistance)
            {
                lowestManhattanDistance = manhattanDistance;
                target = building.position;
            }
        }

        return target;
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
    {get; set;}

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count
    { get; set; }

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position 
    { get; set;}
    

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID
    { get; set; }


    /// <summary>
    /// int property for the building cost.
    /// </summary>
    public int peopleCost
    {get; set;}

    public int woodCost
    {get; set;}

    public int metalCost
    {get; set;}

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; }

    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(Tilemap buildingMap);

    /// <summary>
    /// Removes the current building from the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be removed from.</param>
    public void RemoveBuilding(Tilemap buildingMap);

    /// <summary>
    /// Outputs the appropriate resources for the building to a ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to modify.</param>
    public void OutputResources(ResourceManager resourceManager);
}

/// <summary>
/// The building that get built on a forest.
/// </summary>
public class LumberMill : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count
    { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position
    { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID
    { get; set;}

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 3;
    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 0;
    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; } = 2;

    public LumberMill(Vector3Int position)
    {
        this.position = position;
        ID = "LumberMill " + count;
        count++;
    }

    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the current building from the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be removed from.</param>
    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    /// <summary>
    /// Outputs the appropriate resources for the building to a ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to modify.</param>
    public void OutputResources(ResourceManager resourceManager)
    {
        resourceManager.woodCount++;
    }
}

/// <summary>
/// The building that gets built on water.
/// </summary>
public class Dock : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count
    { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position
    { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID
    { get; set; }

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; } = 2;

    public Dock(Vector3Int position)
    {
        this.position = position;
        ID = "Dock " + count;
        count++;
    }


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the current building from the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be removed from.</param>
    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    /// <summary>
    /// Outputs the appropriate resources for the building to a ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to modify.</param>
    public void OutputResources(ResourceManager resourceManager)
    {
        resourceManager.waterCount++;
    }
}

/// <summary>
/// The building that get built on mountains.
/// </summary>
public class Quarry : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count
    { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position
    { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID
    { get; set; }

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; } = 2;

    public Quarry(Vector3Int position)
    {
        this.position = position;
        ID = "Quarry " + count;
        count++;
    }


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the current building from the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be removed from.</param>
    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    /// <summary>
    /// Outputs the appropriate resources for the building to a ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to modify.</param>
    public void OutputResources(ResourceManager resourceManager)
    {
        resourceManager.metalCount++;
    }
}

/// <summary>
/// The building that gets built on grasslands.
/// </summary>
public class House : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count
    { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position
    { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID
    { get; set; }

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 0;
    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; } = 2;

    public House(Vector3Int position)
    {
        this.position = position;
        ID = "House " + count;
        count++;
    }


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the current building from the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be removed from.</param>
    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    /// <summary>
    /// Outputs the appropriate resources for the building to a ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to modify.</param>
    public void OutputResources(ResourceManager resourceManager)
    {
        resourceManager.foodCount++;
    }
}

/// <summary>
/// The building that gets built on grasslands.
/// </summary>
public class Farm : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count
    { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position
    { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID
    { get; set; }

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; } = 2;

    public Farm(Vector3Int position)
    {
        this.position = position;
        ID = "Farm " + count;
        count++;
    }


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the current building from the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be removed from.</param>
    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    /// <summary>
    /// Outputs the appropriate resources for the building to a ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to modify.</param>
    public void OutputResources(ResourceManager resourceManager)
    {
        resourceManager.foodCount += 10;
    }
}

/// <summary>
/// The building that gets built on grasslands.
/// </summary>
public class Barracks : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count
    { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position
    { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID
    { get; set; }

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 0;
    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 3;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; } = 3;

    public Barracks(Vector3Int position)
    {
        this.position = position;
        ID = "Barracks " + count;
        count++;
    }


    /// <summary>
    /// Add the current building to the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be added to.</param>
    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the current building from the tilemap.
    /// </summary>
    /// <param name="buildingMap">The tilemap for buildings that the building will be removed from.</param>
    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    /// <summary>
    /// Outputs the appropriate resources for the building to a ResourceManager.
    /// </summary>
    /// <param name="resourceManager">The ResourceManager to modify.</param>
    public void OutputResources(ResourceManager resourceManager)
    {
        resourceManager.peopleCount += 5;
    }
}
