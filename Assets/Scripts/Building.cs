using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class buildingContext : MonoBehaviour
{
    private IBuilding building;
    [SerializeField] internal TileBase waterBuildingTile, forestTileBuilding, mountainTileBuilding, grasslandTileBuilding;

    public TileBase getWaterBuildingTile()
    {
        return waterBuildingTile;
    }

    public TileBase getForestBuildingTile()
    {
        return forestTileBuilding;
    }

    public TileBase getMountainBuildingTile()
    {
        return mountainTileBuilding;
    }

    public TileBase getGrasslandBuildingTile()
    {
        return grasslandTileBuilding;
    }

    public void setBuilding(IBuilding newBuilding)
    {
        this.building = newBuilding;
    }

    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        this.building.addBuildingToTile(tilePosition, buildingMap);
    }
}

public interface IBuilding
{
    public TileBase tile
    {
        get;
        set;
    }
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap);
}

public class ForestBuilding : IBuilding
{
    public TileBase tile { get; set; }
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile);
    }
}

public class WaterBuilding : IBuilding
{
    public TileBase tile { get; set; }
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile);
    }
}

public class MountainBuilding : IBuilding
{
    public TileBase tile { get; set; }
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile) ;
    }
}

public class GrasslandBuilding : IBuilding
{
    public TileBase tile { get; set; }
    public void addBuildingToTile(Vector3Int tilePosition, Tilemap buildingMap)
    {
        buildingMap.SetTile(tilePosition, tile);
    }
}
