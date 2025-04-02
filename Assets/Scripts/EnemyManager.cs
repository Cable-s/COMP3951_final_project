using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private Dictionary<string, IEnemy> enemyDict;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyDict = new Dictionary<string, IEnemy>();

        Brute.tile = enemyTile;
        Brute.enemiesMap = enemiesMap;
        Brute.buildingManager = buildingManager;

        IEnemy enemy1 = new Brute(new Vector3Int(-5, -10, 0));
        IEnemy enemy2 = new Brute(new Vector3Int(-12, 12, 0));

        enemyDict.Add(enemy1.ID, enemy1);
        enemyDict.Add(enemy2.ID, enemy2);

        updateEnemies();
    }

    public void updateEnemies()
    {
        enemiesMap.ClearAllTiles();

        foreach (IEnemy enemy in enemyDict.Values)
        {
            enemy.Act();
        }
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

    public static int count;
    public string ID { get; set; }
    public Vector3Int position { get; set; }
    public int damage {  get; set; }
    public int health { get; set; }
    public int speed { get; set; }

    public void Act();
}

public class Brute : IEnemy
{
    /// <summary>
    /// Static TileBase field for the enemy 'sprite'.
    /// </summary>
    public static TileBase tile { get; set; }
    public static Tilemap enemiesMap { get; set; }
    public static BuildingManager buildingManager { get; set; }
    private static int count;
    public string ID { get; set; }
    public Vector3Int position { get; set; }
    public int damage { get; set; }
    public int health { get; set; }
    public int speed { get; set; }

    private Vector3Int? targetPosition;

    public Brute(Vector3Int position)
    {
        this.position = position;
        this.damage = 2;
        this.health = 10;
        this.speed = 1;
        ID = "Brute " + count;
        count++;
    }

    public void Act()
    {
        // Attempt to get a target if none exists, otherwise move to the target.
        targetPosition = buildingManager.GetNearestBuilding(position);

        if(targetPosition == position)
        {
            buildingManager.RemoveBuiding(position);
        }

        if (targetPosition == null)
        {
            MoveRandom();
        }
        else
        {
            MoveToTarget();
        }
    }

    private void MoveRandom()
    {
        enemiesMap.SetTile(position, null);
        position = position + new Vector3Int(speed, 0, 0);
        enemiesMap.SetTile(position, tile);
    }

    private void MoveToTarget()
    {
        //Check if targetPosition is null
        if (targetPosition == null)
        {
            return;
        }

        enemiesMap.SetTile(position, null);

        // Calculate the absolute difference between the two positions
        Vector3Int difference = targetPosition.Value - position;

        //Move towards in x and y if applicable
        if(difference.x > 0)
        {
            position = new Vector3Int (position.x + 1, position.y, position.z);
        }
        else if (difference.x < 0)
        {
            position = new Vector3Int(position.x - 1, position.y, position.z);
        }

        if (difference.y > 0)
        {
            position = new Vector3Int(position.x, position.y + 1, position.z);
        }
        else if (difference.y < 0)
        {
            position = new Vector3Int(position.x, position.y - 1, position.z);
        }

        enemiesMap.SetTile(position, tile);
    }
}
