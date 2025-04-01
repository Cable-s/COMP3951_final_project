using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Tilemap enemiesMap;
    [SerializeField] private TileBase enemyTile;

    private List<Enemy> EnemyList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyList = new List<Enemy>();
        Enemy enemy1 = new Brute(Vector3Int.zero, "JASON");
        Enemy enemy2 = new Brute(new Vector3Int(0, 2, 0), "BINGUS");

        EnemyList.Add(enemy1);
        EnemyList.Add(enemy2);

        updateEnemies();
    }

    public void updateEnemies()
    {
        enemiesMap.ClearAllTiles();

        foreach (Enemy enemy in EnemyList)
        {
            Vector3Int newPosition = enemy.position + new Vector3Int(enemy.speed, 0, 0);
            enemy.position = newPosition;
            SetEnemy(enemy);
        }
    }

    private void SetEnemy(Enemy enemy)
    {
        Vector3Int position = enemy.position;
        enemiesMap.SetTile(position, enemyTile);
    }
}

public interface Enemy
{
    public string name { get; set; }
    public Vector3Int position { get; set; }
    public int damage {  get; set; }
    public int health { get; set; }
    public int speed { get; set; }
}

public class Brute : Enemy
{
    public string name { get; set; }
    public Vector3Int position { get; set; }
    public int damage { get; set; }
    public int health { get; set; }
    public int speed { get; set; }

    public Brute(Vector3Int position, string name)
    {
        this.position = position;
        this.name = name;
        this.damage = 2;
        this.health = 10;
        this.speed = 1;
    }
}
