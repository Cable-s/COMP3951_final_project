using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    /// int property for the amount of required metal to build.
    /// </summary>
    public int stoneCost { get; set; } = 1;

    /// <summary>
    /// The radius of vision for the building
    /// </summary>
    public int sight
    { get; set; } = 2;

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
    /// int property for the amount of required stone to build.
    /// </summary>
    public int stoneCost { get; set; } = 0;
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
    /// int property for the amount of required metal to build.
    /// </summary>    
    public int stoneCost { get; set; } = 3;

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
        return;
    }
}


public class Townhall : IBuilding
{
    public Vector3Int position { get; set; }
    public string ID { get ; set; }
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

    public void AddBuildingToTile(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, tile);
    }

    public void RemoveBuilding(Tilemap buildingMap)
    {
        buildingMap.SetTile(position, null);
    }

    public void OutputResources(ResourceManager resourceManager)
    {
        return;
    }
}