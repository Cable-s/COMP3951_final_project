using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;


[TestFixture]
public class BuildingManagerTests : MonoBehaviour
{
    private BuildingManager manager = new BuildingManager();
    private ResourceManager resourceManager;
    private MapGenerator mapGenerator;
    private Tilemap buildingMap;



    // SetUp is used to prepare the test environment before each test
    [SetUp]
    public void SetUp()
    {
        var resourceManagerPrefab = Resources.Load<ResourceManager>("ResourceManager");
        resourceManager = GameObject.Instantiate(resourceManagerPrefab);

        var mapGeneratorPrefab = Resources.Load<MapGenerator>("Grid");
        mapGenerator = GameObject.Instantiate(mapGeneratorPrefab);

        var buildingMapPrefab = Resources.Load<Tilemap>("Buildings Map");
        buildingMap = GameObject.Instantiate(buildingMapPrefab);

        var tilePrefab = Resources.Load<TileBase>("Barracks_0");
        Farm.tile = GameObject.Instantiate(tilePrefab);
        LumberMill.tile = GameObject.Instantiate(tilePrefab);
        House.tile = GameObject.Instantiate(tilePrefab);
        Barracks.tile = GameObject.Instantiate(tilePrefab);
        Mine.tile = GameObject.Instantiate(tilePrefab);
        Quarry.tile = GameObject.Instantiate(tilePrefab);
        Dock.tile = GameObject.Instantiate(tilePrefab);
        Townhall.tile = GameObject.Instantiate(tilePrefab);

        manager.mapGenerator = mapGenerator;
        manager.buildingMap = buildingMap;
        manager.resourceManager = resourceManager;
        manager.buildingDict.Clear();

    }

    // Test 1: Ensure LumberMill is added when sufficient resources are available
    [Test]
    public void AddLumberMill_WhenSufficientResources_AddsBuildingToDictionaryAndDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "LumberMill";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsTrue(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(7, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(10, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(10, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(10, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map adds building
        Assert.IsNotNull(buildingMap.GetTile(position));

    }

    // Test 2: Ensure Dock is added when sufficient resources are available
    [Test]
    public void AddDock_WhenSufficientResources_AddsBuildingToDictionaryAndDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Dock";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsTrue(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(9, resourceManager.peopleCount);  // 1 person cost
        Assert.AreEqual(9, resourceManager.woodCount);    // 1 wood cost
        Assert.AreEqual(10, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(10, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map adds building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 3: Ensure Quarry is added when sufficient resources are available
    [Test]
    public void AddQuarry_WhenSufficientResources_AddsBuildingToDictionaryAndDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Quarry";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsTrue(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(9, resourceManager.peopleCount);  // 1 person cost
        Assert.AreEqual(8, resourceManager.woodCount);    // 2 wood cost
        Assert.AreEqual(10, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(10, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map adds building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 4: Ensure Mine is added when sufficient resources are available
    [Test]
    public void AddMine_WhenSufficientResources_AddsBuildingToDictionaryAndDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Mine";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsTrue(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(9, resourceManager.peopleCount);  // 1 person cost
        Assert.AreEqual(9, resourceManager.woodCount);    // 1 wood cost
        Assert.AreEqual(10, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(9, resourceManager.stoneCount);   // 1 stone cost

        // Assert: Ensure building map adds building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 5: Ensure House is added when sufficient resources are available
    [Test]
    public void AddHouse_WhenSufficientResources_AddsBuildingToDictionaryAndDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "House";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsTrue(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(13, resourceManager.peopleCount);  // 0 person cost
        Assert.AreEqual(9, resourceManager.woodCount);    // 1 wood cost
        Assert.AreEqual(10, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(10, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map adds building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 6: Ensure Farm is added when sufficient resources are available
    [Test]
    public void AddFarm_WhenSufficientResources_AddsBuildingToDictionaryAndDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Farm";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsTrue(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(9, resourceManager.peopleCount);  // 1 person cost
        Assert.AreEqual(9, resourceManager.woodCount);    // 1 wood cost
        Assert.AreEqual(10, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(10, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map adds building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 7: Ensure LumberMill is added when sufficient resources are available
    [Test]
    public void AddBarracks_WhenSufficientResources_AddsBuildingToDictionaryAndDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Barracks";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsTrue(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(9, resourceManager.peopleCount);  // 1 person cost
        Assert.AreEqual(10, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(7, resourceManager.metalCount);   // 3 metal cost
        Assert.AreEqual(7, resourceManager.stoneCount);   // 3 stone cost

        // Assert: Ensure building map adds building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }



    // Test 8: Ensure LumberMill is not added when insuffient resources are available
    [Test]
    public void AddLumberMill_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "LumberMill";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 9: Ensure Dock is not added when insuffient resources are available
    [Test]
    public void AddDock_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Dock";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 10: Ensure Quarry is not added when insuffient resources are available
    [Test]
    public void AddQuarry_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Quarry";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 11: Ensure Mine is not added when insuffient resources are available
    [Test]
    public void AddMine_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Mine";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 12: Ensure House is not added when insuffient resources are available
    [Test]
    public void AddHouse_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "House";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 13: Ensure Farm is not added when insuffient resources are available
    [Test]
    public void AddFarm_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Farm";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 14: Ensure Barracks is not added when insuffient resources are available
    [Test]
    public void AddBarracks_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = "Barracks";
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 3 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 15: Ensure Barracks is not added when insuffient resources are available
    [Test]
    public void AddNullBuilding_WhenSufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = null;
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(10, resourceManager.peopleCount);  // 0 person cost
        Assert.AreEqual(10, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(10, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(10, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 16: Ensure Barracks is not added when insuffient resources are available
    [Test]
    public void AddNullBuilding_WhenInsufficientResources_DoesNotAddsBuildingToDictionaryAndDoesNotDeductsResources()
    {
        // Arrange: Define the building type and position
        string buildingType = null;
        Vector3Int position = new Vector3Int(0, 0, 0);
        resourceManager.peopleCount = 0;
        resourceManager.woodCount = 0;
        resourceManager.metalCount = 0;
        resourceManager.stoneCount = 0;

        // Act: Call AddBuilding to add a new building
        manager.AddBuilding(buildingType, position);

        // Assert: Ensure the building was added to the dictionary
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: Ensure resources are deducted correctly
        Assert.AreEqual(0, resourceManager.peopleCount);  // 0 person cost
        Assert.AreEqual(0, resourceManager.woodCount);    // 0 wood cost
        Assert.AreEqual(0, resourceManager.metalCount);   // 0 metal cost
        Assert.AreEqual(0, resourceManager.stoneCount);   // 0 stone cost

        // Assert: Ensure building map removes building
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 17: remove resource building (they all are the same) at position 0,0,0. 
    [Test]
    public void RemoveBuilding_ResourceBuilding_Position000_BuildingAtPosition()
    {
        // Arrange: Define building position and add building for removal.
        Vector3Int position = new Vector3Int(0, 0, 0);
        string buildingType = "LumberMill";
        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;
        manager.AddBuilding(buildingType, position); //any resource building will work here.

        // Act: Call RemoveBuilding to remove that building
        manager.RemoveBuiding(position);

        // Assert: people and population are updated correctly
        Assert.AreEqual(10, resourceManager.peopleCount);     //resource back to where it started
        Assert.AreEqual(10, resourceManager.populationCount); //resource back to where it started

        // Assert: buildingDict does not contain building
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: building map removed building at position, null means no building.
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 18: remove house building at position 0,0,0. 
    [Test]
    public void RemoveBuilding_HouseBuilding_Position000_BuildingAtPosition_PeopleCountDoesNotFallBelowZero()
    {
        // Arrange: Define building position and add building for removal.
        Vector3Int position = new Vector3Int(0, 0, 0);
        string buildingType = "House";
        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;
        manager.AddBuilding(buildingType, position); //any resource building will work here.

        // Act: Call RemoveBuilding to remove that building
        manager.RemoveBuiding(position);

        // Assert: people and population are updated correctly
        Assert.AreEqual(12, resourceManager.peopleCount);     //resource back to where it started
        Assert.AreEqual(12, resourceManager.populationCount); //resource back to where it started

        // Assert: buildingDict does not contain building
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: building map removed building at position, null means no building.
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 19: remove house building at position 0,0,0. 
    [Test]
    public void RemoveBuilding_HouseBuilding_Position000_BuildingAtPosition_PeopleCountFallsBelowZero()
    {
        // Arrange: Define building position and add building for removal.
        Vector3Int position = new Vector3Int(0, 0, 0);
        string buildingType = "House";
        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;
        manager.AddBuilding(buildingType, position); //any resource building will work here.
        resourceManager.peopleCount = 0; //simulates adding different buildings, now 0 people


        // Act: Call RemoveBuilding to remove that building
        manager.RemoveBuiding(position);

        // Assert: people and population are updated correctly
        Assert.AreEqual(0, resourceManager.peopleCount);      //people resource lowest should be 0
        Assert.AreEqual(12, resourceManager.populationCount); //resource back to where it started

        // Assert: buildingDict does not contain building
        Assert.IsFalse(manager.buildingDict.ContainsKey(position));

        // Assert: building map removed building at position, null means no building.
        Assert.IsNull(buildingMap.GetTile(position));
    }

    // Test 20: Test helper method AddBuildingToTile
    [Test]
    public void AddBuildingToTile_Farm()
    {
        // Arrange: position and building
        Vector3Int position = new Vector3Int(0, 0, 0);
        IBuilding building = new Farm(position);

        // Act: add building
        manager.AddBuildingToTile(building);

        // Assert: building map added the building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 21: Test helper method AddBuildingToTile
    [Test]
    public void AddBuildingToTile_LumberMill()
    {
        // Arrange: position and building
        Vector3Int position = new Vector3Int(0, 0, 0);
        IBuilding building = new LumberMill(position);

        // Act: add building
        manager.AddBuildingToTile(building);

        // Assert: building map added the building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 22: Test helper method AddBuildingToTile
    [Test]
    public void AddBuildingToTile_Barracks()
    {
        // Arrange: position and building
        Vector3Int position = new Vector3Int(0, 0, 0);
        IBuilding building = new Barracks(position);

        // Act: add building
        manager.AddBuildingToTile(building);

        // Assert: building map added the building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 23: Test helper method AddBuildingToTile
    [Test]
    public void AddBuildingToTile_Dock()
    {
        // Arrange: position and building
        Vector3Int position = new Vector3Int(0, 0, 0);
        IBuilding building = new Dock(position);

        // Act: add building
        manager.AddBuildingToTile(building);

        // Assert: building map added the building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 24: Test helper method AddBuildingToTile
    [Test]
    public void AddBuildingToTile_Quarry()
    {
        // Arrange: position and building
        Vector3Int position = new Vector3Int(0, 0, 0);
        IBuilding building = new Quarry(position);

        // Act: add building
        manager.AddBuildingToTile(building);

        // Assert: building map added the building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 25: Test helper method AddBuildingToTile
    [Test]
    public void AddBuildingToTile_Mine()
    {
        // Arrange: position and building
        Vector3Int position = new Vector3Int(0, 0, 0);
        IBuilding building = new Mine(position);

        // Act: add building
        manager.AddBuildingToTile(building);

        // Assert: building map added the building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 26: Test helper method AddBuildingToTile
    [Test]
    public void AddBuildingToTile_House()
    {
        // Arrange: position and building
        Vector3Int position = new Vector3Int(0, 0, 0);
        IBuilding building = new House(position);
        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;

        // Act: add building
        manager.AddBuildingToTile(building);

        // Assert: people and population update correctly when adding House
        Assert.AreEqual(13, resourceManager.peopleCount);
        Assert.AreEqual(13, resourceManager.populationCount);

        // Assert: building map added the building
        Assert.IsNotNull(buildingMap.GetTile(position));
    }

    // Test 27: Test OutputResources with one farm
    [Test]
    public void OutputResources_OneFarm()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0,0,0), Farm.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Farm(new Vector3Int(0,0,0)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(20, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);

    }

    // Test 28: Test OutputResources with three farms
    [Test]
    public void OutputResources_ThreeFarms()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Farm.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Farm(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(0, 0, 1), Farm.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 1), new Farm(new Vector3Int(0, 0, 1)));

        buildingMap.SetTile(new Vector3Int(0, 1, 1), Farm.tile);
        manager.buildingDict.Add(new Vector3Int(0, 1, 1), new Farm(new Vector3Int(0, 1, 1)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(40, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);
    }

    // Test 29: Test OutputResources with one farm
    [Test]
    public void OutputResources_OneLumberMill()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), LumberMill.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new LumberMill(new Vector3Int(0, 0, 0)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(11, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);

    }

    // Test 30: Test OutputResources with three farms
    [Test]
    public void OutputResources_ThreeLumberMills()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), LumberMill.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new LumberMill(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(0, 0, 1), LumberMill.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 1), new LumberMill(new Vector3Int(0, 0, 1)));

        buildingMap.SetTile(new Vector3Int(0, 1, 1), LumberMill.tile);
        manager.buildingDict.Add(new Vector3Int(0, 1, 1), new LumberMill(new Vector3Int(0, 1, 1)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(13, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);
    }

    // Test 31: Test OutputResources with one farm
    [Test]
    public void OutputResources_OneDock()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Dock.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Dock(new Vector3Int(0, 0, 0)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(11, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);

    }

    // Test 32: Test OutputResources with three farms
    [Test]
    public void OutputResources_ThreeDocks()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Dock.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Dock(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(0, 0, 1), Dock.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 1), new Dock(new Vector3Int(0, 0, 1)));

        buildingMap.SetTile(new Vector3Int(0, 1, 1), Dock.tile);
        manager.buildingDict.Add(new Vector3Int(0, 1, 1), new Dock(new Vector3Int(0, 1, 1)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(13, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);
    }

    // Test 33: Test OutputResources with one farm
    [Test]
    public void OutputResources_OneQuarry()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Quarry.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Quarry(new Vector3Int(0, 0, 0)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(11, resourceManager.stoneCount);

    }

    // Test 34: Test OutputResources with three farms
    [Test]
    public void OutputResources_ThreeQuarrys()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Quarry.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Quarry(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(0, 0, 1), Quarry.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 1), new Quarry(new Vector3Int(0, 0, 1)));

        buildingMap.SetTile(new Vector3Int(0, 1, 1), Quarry.tile);
        manager.buildingDict.Add(new Vector3Int(0, 1, 1), new Quarry(new Vector3Int(0, 1, 1)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(13, resourceManager.stoneCount);
    }

    // Test 35: Test OutputResources with one farm
    [Test]
    public void OutputResources_OneMine()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Mine.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Mine(new Vector3Int(0, 0, 0)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(11, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);

    }

    // Test 36: Test OutputResources with three farms
    [Test]
    public void OutputResources_ThreeMines()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Mine.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Mine(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(0, 0, 1), Mine.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 1), new Mine(new Vector3Int(0, 0, 1)));

        buildingMap.SetTile(new Vector3Int(0, 1, 1), Mine.tile);
        manager.buildingDict.Add(new Vector3Int(0, 1, 1), new Mine(new Vector3Int(0, 1, 1)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(13, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);
    }

    // Test 37: Test OutputResources with one farm
    [Test]
    public void OutputResources_OneHouse()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), House.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new House(new Vector3Int(0, 0, 0)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);

    }

    // Test 38: Test OutputResources with three farms
    [Test]
    public void OutputResources_ThreeHouses()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), House.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new House(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(0, 0, 1), House.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 1), new House(new Vector3Int(0, 0, 1)));

        buildingMap.SetTile(new Vector3Int(0, 1, 1), House.tile);
        manager.buildingDict.Add(new Vector3Int(0, 1, 1), new House(new Vector3Int(0, 1, 1)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);
    }

    // Test 39: Test OutputResources with one farm
    [Test]
    public void OutputResources_OneBarracks()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Barracks(new Vector3Int(0, 0, 0)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);

    }

    // Test 40: Test OutputResources with three farms
    [Test]
    public void OutputResources_ThreeBarracks()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Barracks(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(0, 0, 1), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 1), new Barracks(new Vector3Int(0, 0, 1)));

        buildingMap.SetTile(new Vector3Int(0, 1, 1), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(0, 1, 1), new Barracks(new Vector3Int(0, 1, 1)));

        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);
    }

    // Test 41: Test OutputResources with no buildings
    [Test]
    public void OutputResources_NoBuildings()
    {
        // Arrange
        resourceManager.peopleCount = 10;
        resourceManager.populationCount = 10;
        resourceManager.foodCount = 10;
        resourceManager.woodCount = 10;
        resourceManager.metalCount = 10;
        resourceManager.stoneCount = 10;

        // Act
        manager.OutputResources();

        // Assert
        Assert.AreEqual(10, resourceManager.peopleCount);
        Assert.AreEqual(10, resourceManager.populationCount);
        Assert.AreEqual(10, resourceManager.foodCount);
        Assert.AreEqual(10, resourceManager.woodCount);
        Assert.AreEqual(10, resourceManager.metalCount);
        Assert.AreEqual(10, resourceManager.stoneCount);
    }

    // Test 42: Get nearest building with no buildings
    [Test]
    public void GetNearestBuilding_NoBuilding()
    {
        // no arrangement needed

        // Act
        Vector3Int? result = manager.GetNearestBuilding(new Vector3Int(0,0,0));

        // Assert
        Assert.IsNull(result);
    }

    // Test 43: Get nearest building with one building
    [Test]
    public void GetNearestBuilding_OneBuilding()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Barracks(new Vector3Int(0, 0, 0)));

        // Act
        Vector3Int? result = manager.GetNearestBuilding(new Vector3Int(0, 0, 1));

        // Assert
        Assert.AreEqual(new Vector3Int(0,0,0), result);
    }

    // Test 44: Get nearest building with three buildings
    [Test]
    public void GetNearestBuilding_ThreeBuilding()
    {
        // Arrange
        buildingMap.SetTile(new Vector3Int(0, 0, 0), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(0, 0, 0), new Barracks(new Vector3Int(0, 0, 0)));

        buildingMap.SetTile(new Vector3Int(5, 0, 0), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(5, 0, 0), new Barracks(new Vector3Int(5, 0, 0)));

        buildingMap.SetTile(new Vector3Int(-5, 0, 0), Barracks.tile);
        manager.buildingDict.Add(new Vector3Int(-5, 0, 0), new Barracks(new Vector3Int(-5, 0, 0)));

        // Act
        Vector3Int? result = manager.GetNearestBuilding(new Vector3Int(0, 0, 1));
        Vector3Int? result2 = manager.GetNearestBuilding(new Vector3Int(-6, 0, 1));
        Vector3Int? result3 = manager.GetNearestBuilding(new Vector3Int(6, 0, 1));


        // Assert
        Assert.AreEqual(new Vector3Int(0, 0, 0), result);
        Assert.AreEqual(new Vector3Int(-5, 0, 0), result2);
        Assert.AreEqual(new Vector3Int(5, 0, 0), result3);

    }
}
