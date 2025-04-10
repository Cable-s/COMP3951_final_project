using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

///This module is for the various buildings that the player can build.
///<author>Caleb Janzen</author>

/// <summary>
/// Interface for game buildings defining common properties and methods.
/// Author: Caleb Janzen
/// </summary>
public interface IBuilding
{
    /// <summary>
    /// Tile representing the building visually.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// Counts total instances of the building.
    /// </summary>
    public static int count { get; set; }

    /// <summary>
    /// Position of the building on the tilemap.
    /// </summary>
    public Vector3Int position { get; set; }

    /// <summary>
    /// Unique identifier for the building.
    /// </summary>
    public string ID { get; set; }

    /// <summary>
    /// Resource costs for constructing the building.
    /// </summary>
    public int peopleCost { get; set; }
    public int woodCost { get; set; }
    public int metalCost { get; set; }
    public int stoneCost { get; set; }


    /// <summary>
    /// Sight radius provided by the building.
    /// </summary>
    public int sight { get; set; }

    /// <summary>
    /// Adds the building to a given tilemap.
    /// </summary>
    void AddBuildingToTile(Tilemap buildingMap);

    /// <summary>
    /// Removes the building from a given tilemap.
    /// </summary>
    void RemoveBuilding(Tilemap buildingMap);

    /// <summary>
    /// Outputs resources produced by the building.
    /// </summary>
    void OutputResources(ResourceManager resourceManager);
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
    public static int count { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID { get; set; }

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
    /// int property for the amount of required stone to build.
    /// </summary>
    public int stoneCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight { get; set; } = 2;

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
    public static int count { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID { get; set; }

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
    /// int property for the amount of required stone to build.
    /// </summary>
    public int stoneCost { get; set; } = 0;
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
        resourceManager.foodCount++;
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
    public static int count { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID { get; set; }

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 1;

    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 2;

    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 0;

    /// <summary>
    /// int property for the amount of required stone to build.
    /// </summary>
    public int stoneCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight { get; set; } = 2;

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
        resourceManager.stoneCount++;
    }
}


/// <summary>
/// The building that get built on mountains.
/// </summary>
public class Mine : IBuilding
{
    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID { get; set; }

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
    /// int property for the amount of required metal to build.
    /// </summary>
    public int stoneCost { get; set; } = 1;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight { get; set; } = 2;

    public Mine(Vector3Int position)
    {
        this.position = position;
        ID = "Mine " + count;
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
    public static int count { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID { get; set; }

    /// <summary>
    /// int property for the amount of required people to build.
    /// </summary>
    public int peopleCost { get; set; } = 0;
    /// <summary>
    /// int property for the amount of required wood to build.
    /// </summary>
    public int woodCost { get; set; } = 1;
    /// <summary>
    /// int property for the amount of required metal to build.
    /// </summary>
    public int metalCost { get; set; } = 0;
    /// <summary>
    /// int property for the amount of required stone to build.
    /// </summary>
    public int stoneCost { get; set; } = 0;
    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight { get; set; } = 2;

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
        return;
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
    public static int count { get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position { get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID { get; set; }

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
    /// int property for the amount of required stone to build.
    /// </summary>
    public int stoneCost { get; set; } = 0;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight { get; set; } = 2;

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
        resourceManager.foodCount += 3;
    }
}

/// <summary>
/// The building that attacks enemies within its kill range.
/// Built on grasslands, does not output resources.
/// </summary>
public class Barracks : IBuilding
{
    /// <summary>
    /// The enemyManager for identifying and modifying enemies close to the Barracks
    /// </summary>
    public static EnemyManager enemyManager { get; set; }

    /// <summary>
    /// Static TileBase field for the building 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    /// <summary>
    /// The number of buildings of this type that have ever been created. Used for assigning IDs
    /// </summary>
    public static int count{ get; set; } = 0;

    /// <summary>
    /// The position of the tile on the buildingMap
    /// </summary>
    public Vector3Int position{ get; set; }

    /// <summary>
    /// A unique identifier for the building
    /// </summary>
    public string ID { get; set; }

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
    /// int property for the amount of required metal to build.
    /// </summary>    
    public int stoneCost { get; set; } = 3;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight { get; set; } = 3;
    
    /// <summary>
    /// Radius in which enemies are killed
    /// </summary>
    public int killRange { get; set; } = 2;

    private List<Vector3Int> positionsInRange;

    private int killsPerRound = 1;

    public Barracks(Vector3Int position)
    {
        this.position = position;
        ID = "Barracks " + count;
        positionsInRange = PopulatePositionsInRange();
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
        //Shuffle positions in range to avoid bias towards particular tiles
        ShufflePositionsInRange();

        int killCount = 0;

        //Go through the list in random order until you exhaust the list or find a tile that contains an enemy some number of times
        //If a searched tile contains an enemy kill the enemy
        foreach (Vector3Int position in positionsInRange)
        {
            if(enemyManager.positionContainsEnemy(position))
            {
                IEnemy enemy = enemyManager.getEnemy(position);

                enemy.Die();

                if(killCount == killsPerRound - 1)
                {
                    break;
                }
                else
                {
                    count++;
                } 
            }
        }
    }

    private List<Vector3Int> PopulatePositionsInRange()
    {
        List<Vector3Int> positionsInRange = new List<Vector3Int>();

        //Generate a list of positions in Vector3Int form that represent the tiles the Barracks can hit around it
        // Loop over a square region around the center cell.
        for (int x = -killRange; x <= killRange; x++)
        {
            for (int y = -killRange; y <= killRange; y++)
            {
                Vector3Int cell = position + new Vector3Int(x, y, 0);

                // Only add cells within the circular area defined by the killRange radius.
                if (Vector3Int.Distance(position, cell) <= killRange)
                {
                    positionsInRange.Add(cell);
                }
            }
        }

        return positionsInRange;
    }

    private void ShufflePositionsInRange()
    {
        int x = positionsInRange.Count;
        while (x > 1)
        {
            x--;
            int k = Random.Range(0, x + 1);

            Vector3Int value = positionsInRange[x];
            positionsInRange[x] = positionsInRange[k];
            positionsInRange[k] = value;
        }
    }
}

/// <summary>
/// The main building that may be tied to win/loss conditions.
/// </summary>
public class Townhall : IBuilding
{
    public Vector3Int position { get; set; }
    public string ID { get; set; }

    public int peopleCost { get; set; }
    public int woodCost { get; set; }
    public int metalCost { get; set; }
    public int stoneCost { get; set; }
    public int sight { get; set; }

    public static TileBase tile { get; set; }
    public static int count { get; set; } = 0;

    public Townhall(Vector3Int position)
    {
        this.position = position;
        ID = "Townhall: " + count;
        count++;
    }

    /// <summary>
    /// Adds the townhall tile to the tilemap.
    /// </summary>
    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the townhall tile from the tilemap.
    /// </summary>
    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    /// <summary>
    /// Townhall does not produce resources.
    /// </summary>
    public void OutputResources(ResourceManager resourceManager)
    {
        return;
    }
}