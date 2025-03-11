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
        print("next day");
        //increment day count
        resourceManager.dayCount++;
        BoundsInt bounds = buildingMap.cellBounds;
        //get all buildings on board
        for (int x = bounds.x; x < bounds.xMax; x++)
        {
            for (int y = bounds.y; y < bounds.yMax; y++)
            {
                if (buildingMap.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    //add that buildings resource to running count
                    switch (terrainMap.GetTile(new Vector3Int(x, y, 0)).name)
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

