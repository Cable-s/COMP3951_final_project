using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class NextDay : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Tilemap buildingMap;
    [SerializeField] private Tilemap terrainMap;

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(nextDay);
    }

    void nextDay()
    {
        print("next day");
        //increment day count
        resourceManager.dayCount++;
        print(resourceManager.dayCount);

        //have the BuildingManager output resources for the day
        buildingManager.OutputResources();

        //update UI for resources to have updated 
        resourceManager.updateResourceCountText();
        enemyManager.updateEnemies();
    }
}

