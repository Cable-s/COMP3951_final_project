using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Codice.CM.Client.Differences.Merge;
using JetBrains.Annotations;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Tilemap enemiesMap;
    [SerializeField] private TileBase enemyTile;

    [Header("Managers")]
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private ResourceManager resourceManager;

    private Dictionary<string, IEnemy> enemyDict;
    private Dictionary<Vector3Int, IEnemy> enemyPositionDict;
    private int minX;
    private int maxX;
    private int minY;
    private int maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Create dictionary to store enemy objects
        enemyDict = new Dictionary<string, IEnemy>();

        Brute.tile = enemyTile;
        Brute.enemiesMap = enemiesMap;
        Brute.buildingManager = buildingManager;
        Brute.enemyManager = this;
        
        //Get boundaries of map for enemy spawning purposes
        minX = 0 - mapGenerator.width / 2;
        maxX = (mapGenerator.width - 1) - mapGenerator.width / 2;
        minY = 0 - mapGenerator.height / 2;
        maxY = (mapGenerator.height - 1) - mapGenerator.height / 2;

        //updateEnemies();
    }

    public void updateEnemies()
    {
        foreach (IEnemy enemy in enemyDict.Values)
        {
            enemy.Act();
        }

        //for (int x = 0; x < 2; x++)
        //{
        //    SpawnEnemy();
        //}
        if(resourceManager.dayCount % 5 == 0 && resourceManager.dayCount > 0)
        {
            int enemiesToSpawn = resourceManager.dayCount / 4;

            if (resourceManager.dayCount > 50)
            {
                enemiesToSpawn += (int)Mathf.Pow(1.05f, (resourceManager.dayCount - 50)) - 1;
            }

            for (int x = 0; x < enemiesToSpawn; x++)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        //Get a tile location on the perimeter of the map
        Vector3Int perimeterPosition = getPerimeterPosition();

        //Create a new enemy and add to dictionary at that location if not already occupied
        if(!positionContainsEnemy(perimeterPosition))
        {
            IEnemy newEnemy = new Brute(perimeterPosition);

            //Add to dictionary
            addEnemy(newEnemy);
        }
    }

    private Vector3Int getPerimeterPosition()
    {
        //Represents which edge to generate the enemy [0=Left, 1=Right, 2=Bottom, 3=Top]
        int edge = Random.Range(0,4);

        //Random value from minX to maxY for use with top and bottom edges
        int randY = Random.Range(minX,maxX + 1);

        //Random value from minX to maxY for use with left and right edges
        int randX = Random.Range(minY,maxY + 1);

        Vector3Int position;

        switch (edge)
        {
            //Left Edge
            case 0: position = new Vector3Int(minX, randY, 0); break;

            //Right Edge
            case 1: position = new Vector3Int(maxX, randY, 0); break;

            //Bottom Edge
            case 2: position = new Vector3Int(randX, minY, 0); break;

            //Top Edge
            case 3: position = new Vector3Int(randX, maxY, 0); break;

            default: position = Vector3Int.zero; break;
        }

        return position;
    }

    public bool positionContainsEnemy(Vector3Int position)
    {
        foreach (IEnemy enemy in enemyDict.Values)
        {
            if (enemy.position == position)
            {
                return true;
            }
        }

        return false;
    }

    public void removeEnemy(Vector3Int position)
    {
        foreach (IEnemy enemy in enemyDict.Values)
        {
            if (enemy.position == position)
            {
                enemyDict.Remove(enemy.ID);
                break;
            }
        }
    }

    public void removeEnemy(string id)
    {
        enemyDict.Remove(id);
    }

    public void addEnemy(IEnemy enemy)
    {
        enemyDict.Add(enemy.ID, enemy);
    }

    public IEnemy getEnemy(Vector3Int position)
    {
        foreach (IEnemy enemy in enemyDict.Values)
        {
            if (enemy.position == position)
            {
                return enemy;
            }
        }

        return null;
    }
}

public interface IEnemy
{
    /// <summary>
    /// Static TileBase field for the enemy 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }

    public static Tilemap enemiesMap { get; set; }

    public static BuildingManager buildingManager { get; set; }

    public static EnemyManager enemyManager { get; set; }

    public static int count;
    public string ID { get; set; }
    public Vector3Int position { get; set; }
    public int damage {  get; set; }
    public int health { get; set; }
    public int speed { get; set; }

    public bool isAlive { get; set; }

    public void Act();

    public void Die();
}

public class Brute : IEnemy
{
    /// <summary>
    /// Static TileBase field for the enemy 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }
    public static Tilemap enemiesMap { get; set; }
    public static BuildingManager buildingManager { get; set; }
    public static EnemyManager enemyManager { get; set; }
    private static int count;
    public string ID { get; set; }
    public Vector3Int position { get; set; }
    public int damage { get; set; }
    public int health { get; set; }
    public int speed { get; set; }

    public bool isAlive { get; set; }

    private Vector3Int? targetPosition;

    public Brute(Vector3Int position)
    {
        this.position = position;
        this.damage = 2;
        this.health = 10;
        this.speed = 1;
        this.isAlive = true;
        ID = "Brute " + count;
        count++;
        enemiesMap.SetTile(position, tile);
    }

    /// <summary>
    /// Main enemy behavior each frame. Gets target and decides to move or attack.
    /// </summary>
    public void Act()
    {
        // Attempt to get a target if none exists, otherwise move to the target.
        targetPosition = buildingManager.GetNearestBuilding(position);

        // If already on a building tile, destroy it.
        if(targetPosition == position)
        {
            buildingManager.RemoveBuiding(position);
        }

        // If there's no building, wander randomly.
        if (targetPosition == null)
        {
            MoveRandom();
        }
        else
        {
            MoveToTarget(); // If there is building, move to it. 
        }
    }

    /// <summary>
    /// Moves enemy in a straight line (used when no target exists).
    /// </summary>
    private void MoveRandom()
    {
        Vector3Int nextPosition = position + new Vector3Int(speed, 0, 0);

        //Check if nextPosition has an enemy already, if so, set nextPosition to currentPosition ie. don't move
        if (enemyManager.positionContainsEnemy(nextPosition))
        {
            return;
        }

        enemiesMap.SetTile(position, null);
        position = nextPosition;
        enemiesMap.SetTile(position, tile);
    }

    /// <summary>
    /// Moves the enemy one step toward its target. Dies if entering a Barrakcs kill range.
    /// </summary>
    private void MoveToTarget()
    {
        //Check if targetPosition within this enemy object is null
        if (targetPosition == null)
        {
            return;
        }

        // Calculate the absolute difference between the two positions
        Vector3Int difference = targetPosition.Value - position;

        // Initialize next position as current for modification
        Vector3Int nextPosition = position;

        //Set next position one unit towards x and y if applicable
        if (difference.x > 0)
        {
            nextPosition.x += 1;
        }
        else if (difference.x < 0)
        {
            nextPosition.x -= 1;
        }

        if (difference.y > 0)
        {
            nextPosition.y += 1;
        }
        else if (difference.y < 0)
        {
            nextPosition.y -= 1;
        }

        //Check if nextPosition has an enemy already, if so, set nextPosition to currentPosition ie. don't move
        if(enemyManager.positionContainsEnemy(nextPosition))
        {
            return;
        }

        //Clears the current position of the enemy sprite
        enemiesMap.SetTile(position, null);

        // Move enemy and draw tile at new position
        position = nextPosition;
        enemiesMap.SetTile(position, tile);
    }

    /// <summary>
    /// Removes the enemy from the map and game (enemyManager dict). 
    /// </summary>
    public void Die()
    {
        enemiesMap.SetTile(position, null);

        enemyManager.removeEnemy(ID);

        Debug.Log($"{ID} was killed within Barracks kill range.");
    }
}
