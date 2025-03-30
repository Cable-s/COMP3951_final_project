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
        print(resourceManager.dayCount);
        BoundsInt bounds = buildingMap.cellBounds;
        //get all buildings on board
        for (int x = bounds.x; x < bounds.xMax; x++)
        {
            for (int y = bounds.y; y < bounds.yMax; y++)
            {
                if (buildingMap.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    //add that buildings resource to running count
                    switch (buildingMap.GetTile(new Vector3Int(x, y, 0)).name)
                    {
                        case "forestBuilding":
                            resourceManager.woodCount++;
                            break;
                        case "mountainBuilding":
                            resourceManager.metalCount++;
                            break;
                        case "house_0":
                            resourceManager.foodCount++;
                            break;
                        case "boat2_0":
                            resourceManager.waterCount++;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        //update UI for resources to have updated 
        resourceManager.updateResourceCountText();
    }
}

