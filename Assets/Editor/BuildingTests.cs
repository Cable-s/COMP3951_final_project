using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;

public class BuildingTests
{
    [Test]
    public void LumberMill_OutputResources()
    {
        //set up resource manager
        ResourceManager rm = new ResourceManager();
        rm.woodCount = 10;
        rm.metalCount = 10;
        rm.stoneCount = 10;
        rm.foodCount = 10;

        //setup expected values
        int expectedWoodCount = 11;
        int expectedMetalCount = 10;
        int expectedStoneCount = 10;
        int expectedFoodCount = 10;

        //create object to test
        IBuilding building = new LumberMill(new Vector3Int(0,0,0));

        //run method to test
        building.OutputResources(rm);

        //assert ResourceManager resources updated
        Assert.AreEqual(expectedWoodCount, rm.woodCount);
        Assert.AreEqual(expectedMetalCount, rm.metalCount);
        Assert.AreEqual(expectedStoneCount, rm.stoneCount);
        Assert.AreEqual(expectedFoodCount, rm.foodCount);
    }

    [Test]
    public void Dock_OutputResources()
    {
        //set up resource manager
        ResourceManager rm = new ResourceManager();
        rm.woodCount = 10;
        rm.metalCount = 10;
        rm.stoneCount = 10;
        rm.foodCount = 10;

        //setup expected values
        int expectedWoodCount = 10;
        int expectedMetalCount = 10;
        int expectedStoneCount = 10;
        int expectedFoodCount = 11;

        //create object to test
        IBuilding building = new Dock(new Vector3Int(0, 0, 0));

        //run method to test
        building.OutputResources(rm);

        //assert ResourceManager resources updated
        Assert.AreEqual(expectedWoodCount, rm.woodCount);
        Assert.AreEqual(expectedMetalCount, rm.metalCount);
        Assert.AreEqual(expectedStoneCount, rm.stoneCount);
        Assert.AreEqual(expectedFoodCount, rm.foodCount);
    }

    [Test]
    public void Quarry_OutputResources()
    {
        //set up resource manager
        ResourceManager rm = new ResourceManager();
        rm.woodCount = 10;
        rm.metalCount = 10;
        rm.stoneCount = 10;
        rm.foodCount = 10;

        //setup expected values
        int expectedWoodCount = 10;
        int expectedMetalCount = 10;
        int expectedStoneCount = 11;
        int expectedFoodCount = 10;

        //create object to test
        IBuilding building = new Quarry(new Vector3Int(0, 0, 0));

        //run method to test
        building.OutputResources(rm);

        //assert ResourceManager resources updated
        Assert.AreEqual(expectedWoodCount, rm.woodCount);
        Assert.AreEqual(expectedMetalCount, rm.metalCount);
        Assert.AreEqual(expectedStoneCount, rm.stoneCount);
        Assert.AreEqual(expectedFoodCount, rm.foodCount);
    }

    [Test]
    public void Mine_OutputResources()
    {
        //set up resource manager
        ResourceManager rm = new ResourceManager();
        rm.woodCount = 10;
        rm.metalCount = 10;
        rm.stoneCount = 10;
        rm.foodCount = 10;

        //setup expected values
        int expectedWoodCount = 10;
        int expectedMetalCount = 11;
        int expectedStoneCount = 10;
        int expectedFoodCount = 10;

        //create object to test
        IBuilding building = new Mine(new Vector3Int(0, 0, 0));

        //run method to test
        building.OutputResources(rm);

        //assert ResourceManager resources updated
        Assert.AreEqual(expectedWoodCount, rm.woodCount);
        Assert.AreEqual(expectedMetalCount, rm.metalCount);
        Assert.AreEqual(expectedStoneCount, rm.stoneCount);
        Assert.AreEqual(expectedFoodCount, rm.foodCount);
    }

    [Test]
    public void House_OutputResources()
    {
        //set up resource manager
        ResourceManager rm = new ResourceManager();
        rm.woodCount = 10;
        rm.metalCount = 10;
        rm.stoneCount = 10;
        rm.foodCount = 10;

        //setup expected values
        int expectedWoodCount = 10;
        int expectedMetalCount = 10;
        int expectedStoneCount = 10;
        int expectedFoodCount = 10;

        //create object to test
        IBuilding building = new House(new Vector3Int(0, 0, 0));

        //run method to test
        building.OutputResources(rm);

        //assert ResourceManager resources updated
        Assert.AreEqual(expectedWoodCount, rm.woodCount);
        Assert.AreEqual(expectedMetalCount, rm.metalCount);
        Assert.AreEqual(expectedStoneCount, rm.stoneCount);
        Assert.AreEqual(expectedFoodCount, rm.foodCount);
    }

    [Test]
    public void Farm_OutputResources()
    {
        //set up resource manager
        ResourceManager rm = new ResourceManager();
        rm.woodCount = 10;
        rm.metalCount = 10;
        rm.stoneCount = 10;
        rm.foodCount = 10;

        //setup expected values
        int expectedWoodCount = 10;
        int expectedMetalCount = 10;
        int expectedStoneCount = 10;
        int expectedFoodCount = 20;

        //create object to test
        IBuilding building = new Farm(new Vector3Int(0, 0, 0));

        //run method to test
        building.OutputResources(rm);

        //assert ResourceManager resources updated
        Assert.AreEqual(expectedWoodCount, rm.woodCount);
        Assert.AreEqual(expectedMetalCount, rm.metalCount);
        Assert.AreEqual(expectedStoneCount, rm.stoneCount);
        Assert.AreEqual(expectedFoodCount, rm.foodCount);
    }

    [Test]
    public void Barracks_OutputResources()
    {
        //set up resource manager
        ResourceManager rm = new ResourceManager();
        rm.woodCount = 10;
        rm.metalCount = 10;
        rm.stoneCount = 10;
        rm.foodCount = 10;

        //setup expected values
        int expectedWoodCount = 10;
        int expectedMetalCount = 10;
        int expectedStoneCount = 10;
        int expectedFoodCount = 10;        
        
        //create object to test
        IBuilding building = new Barracks(new Vector3Int(0, 0, 0));

        //run method to test
        building.OutputResources(rm);

        //assert ResourceManager resources updated
        Assert.AreEqual(expectedWoodCount, rm.woodCount);
        Assert.AreEqual(expectedMetalCount, rm.metalCount);
        Assert.AreEqual(expectedStoneCount, rm.stoneCount);
        Assert.AreEqual(expectedFoodCount, rm.foodCount);
    }

    [Test]
    public void LumberMill_AddBuildingToTile()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        LumberMill.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new LumberMill(position);

        //execute method being tested
        building.AddBuildingToTile(buildingMap);

        //assert correct behaviour
        Assert.AreEqual(buildingMap.GetTile(position), LumberMill.tile);
    }

    [Test]
    public void Dock_AddBuildingToTile()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Dock.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Dock(position);

        //execute method being tested
        building.AddBuildingToTile(buildingMap);

        //assert correct behaviour
        Assert.AreEqual(buildingMap.GetTile(position), Dock.tile);
    }

    [Test]
    public void Mine_AddBuildingToTile()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Mine.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Mine(position);

        //execute method being tested
        building.AddBuildingToTile(buildingMap);

        //assert correct behaviour
        Assert.AreEqual(buildingMap.GetTile(position), Mine.tile);
    }

    [Test]
    public void Quarry_AddBuildingToTile()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Quarry.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Quarry(position);

        //execute method being tested
        building.AddBuildingToTile(buildingMap);

        //assert correct behaviour
        Assert.AreEqual(buildingMap.GetTile(position), Quarry.tile);
    }

    [Test]
    public void Farm_AddBuildingToTile()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Farm.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Farm(position);

        //execute method being tested
        building.AddBuildingToTile(buildingMap);

        //assert correct behaviour
        Assert.AreEqual(buildingMap.GetTile(position), Farm.tile);
    }

    [Test]
    public void House_AddBuildingToTile()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        House.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new House(position);

        //execute method being tested
        building.AddBuildingToTile(buildingMap);

        //assert correct behaviour
        Assert.AreEqual(buildingMap.GetTile(position), House.tile);
    }

    [Test]
    public void Barracks_AddBuildingToTile()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Barracks.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Barracks(position);

        //execute method being tested
        building.AddBuildingToTile(buildingMap);

        //assert correct behaviour
        Assert.AreEqual(buildingMap.GetTile(position), Barracks.tile);
    }

    [Test]
    public void LumberMill_RemoveBuilding()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        LumberMill.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new LumberMill(position);

        buildingMap.SetTile(position, LumberMill.tile);

        //execute method being tested
        building.RemoveBuilding(buildingMap);

        //assert correct behaviour 
        Assert.IsNull(buildingMap.GetTile(position));
    }

    [Test]
    public void Dock_RemoveBuilding()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Dock.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Dock(position);

        buildingMap.SetTile(position, Dock.tile);

        //execute method being tested
        building.RemoveBuilding(buildingMap);

        //assert correct behaviour 
        Assert.IsNull(buildingMap.GetTile(position));
    }

    [Test]
    public void Mine_RemoveBuilding()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Mine.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Mine(position);

        buildingMap.SetTile(position, Mine.tile);

        //execute method being tested
        building.RemoveBuilding(buildingMap);

        //assert correct behaviour 
        Assert.IsNull(buildingMap.GetTile(position));
    }

    [Test]
    public void Quarry_RemoveBuilding()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Quarry.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Quarry(position);

        buildingMap.SetTile(position, Quarry.tile);

        //execute method being tested
        building.RemoveBuilding(buildingMap);

        //assert correct behaviour 
        Assert.IsNull(buildingMap.GetTile(position));
    }

    [Test]
    public void Farm_RemoveBuilding()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Farm.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Farm(position);

        buildingMap.SetTile(position, Farm.tile);

        //execute method being tested
        building.RemoveBuilding(buildingMap);

        //assert correct behaviour 
        Assert.IsNull(buildingMap.GetTile(position));
    }

    [Test]
    public void House_RemoveBuilding()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        House.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new House(position);

        buildingMap.SetTile(position, House.tile);

        //execute method being tested
        building.RemoveBuilding(buildingMap);

        //assert correct behaviour 
        Assert.IsNull(buildingMap.GetTile(position));
    }

    [Test]
    public void Barracks_RemoveBuilding()
    {
        //set up prefabs for SerializeFields
        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");

        //this is a random sprite, as it doesn't matter what sprite it is, only that it is there
        var tilePrefab = Resources.Load<TileBase>("Barracks_0");

        //setup objects needed for test
        Tilemap buildingMap = GameObject.Instantiate(buildingMapPrefab);
        Vector3Int position = new Vector3Int(0, 0, 0);
        Barracks.tile = GameObject.Instantiate(tilePrefab);
        IBuilding building = new Barracks(position);

        buildingMap.SetTile(position, Barracks.tile);

        //execute method being tested
        building.RemoveBuilding(buildingMap);

        //assert correct behaviour 
        Assert.IsNull(buildingMap.GetTile(position));
    }
}
