using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;



public class NextDay : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Tilemap buildingMap;
    [SerializeField] private Tilemap terrainMap;
    private Dictionary<Vector3Int, IBuilding> buildings;

    void Start()
    {
        buildings = buildingManager.buildingDict;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(nextDay);
    }

    void nextDay()
    {
        //increment day count
        resourceManager.dayCount++;

        //have the BuildingManager output resources for the day
        buildingManager.OutputResources();

        //people eat food, each person eats one food, else will remove people if there is not enough food
        if (resourceManager.foodCount - resourceManager.peopleCount >= 0)
        {
            resourceManager.foodCount -= resourceManager.peopleCount;
        }


        //update UI for resources to have updated 
        resourceManager.updateResourceCountText();
        enemyManager.updateEnemies();
    }
}

