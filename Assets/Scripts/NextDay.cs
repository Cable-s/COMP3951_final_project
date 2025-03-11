using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class NextDay : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
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
        //increment day count
        resourceManager.dayCount++;

        //get all buildings on board
        for (int i = 0; i < mapGenerator.width; i++)
        {
            for (int j = 0; j < mapGenerator.height; j++)
            {
                if (buildingMap.GetTile(new Vector3Int(i, j, 0))) {
                    //add that buildings resource to running count
                    switch (terrainMap.GetTile(new Vector3Int(i, j, 0)).name)
                    {
                        case "forest_tile_0":
                            resourceManager.woodCount++;
                            break;
                        case "mountain_tile_0":
                            resourceManager.metalCount++;
                            break;
                        case "grassland_tile_0":
                            resourceManager.foodCount++;
                            break;
                        case "water_tile_0":
                            resourceManager.waterCount++;
                            break;
                    }
                }
            }
        }
        //update UI for resources to have updated 
        resourceManager.updateResourceCountText();
    }
}
