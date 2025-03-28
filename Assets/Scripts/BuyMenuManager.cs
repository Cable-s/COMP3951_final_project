using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using System.Net.NetworkInformation;

public class BuyMenuManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private CanvasRenderer card;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private buildingContext buildingContext;
    [SerializeField] private Tilemap buildingMap;
    private TextMeshProUGUI SideBarName;
    private TextMeshProUGUI cardName;
    private TextMeshProUGUI price;
    private TextMeshProUGUI priceAmount;
    private TileBase tile;
    private Vector3Int tilePosition;
    static private bool BuyMenuOpen = false;

    public void toggle()
    {
        if (BuyMenuOpen)
        {
            this.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            BuyMenuOpen = false;
        }
        else
        {
            tile = mapGenerator.Tilemap.GetTile(grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            tilePosition = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            TextMeshProUGUI[] ts = this.transform.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI t in ts)
            {
                if (t.name == "Header") SideBarName = t;
            }
            if (tile == mapGenerator.waterTile)
            {
                SideBarName.text = "Water";
            }
            else if (tile == mapGenerator.grasslandTile)
            {
                SideBarName.text = "Grasslands";

            }
            else if (tile == mapGenerator.forestTile)
            {
                SideBarName.text = "Forest";
            }
            else
            {
                SideBarName.text = "Mountain";
            }
            this.GetComponent<RectTransform>().transform.localScale = new Vector3(0,0,0);
            BuyMenuOpen = true;
            UpdateCard();
        }
    }

    private void UpdateCard()
    {
        TextMeshProUGUI[] ts = this.transform.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI t in ts)
        {
            if (t.name == "CardName") cardName = t;
            if (t.name == "Price") price = t;
            if (t.name == "PriceAmount") priceAmount = t;
        }
        switch (tile.name) {
            case "grassland_tile_0":
                cardName.text = "grassland Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
            case "water_tile_0":
                cardName.text = "water Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
            case "mountain_tile_0":
                cardName.text = "mountain Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
            case "forest_tile_0":
                cardName.text = "forest Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
        }
    }

    public void BuyEvent()
    {
        // Check if this tile is still fogged
        if (mapGenerator.FogOfWar.GetTile(tilePosition) != null)
        {
            Debug.Log("Cannot place building in fogged area!"); // For testing purposes TO BE REMOVED 
            return;
        }

        if (tile.name == "basetile_0")
        {
            IBuilding building = new GrasslandBuilding();
            buildingContext.setBuilding(building);
        }
        else if (tile.name == "water_0")
        {
            IBuilding building = new WaterBuilding();
            buildingContext.setBuilding(building);
        }
        else if (tile.name == "rock_0")
        {
            IBuilding building = new MountainBuilding();
            buildingContext.setBuilding(building);
        }
        else
        {
            IBuilding building = new ForestBuilding();
            buildingContext.setBuilding(building);
        }
        buildingContext.addBuildingToTile(tilePosition, buildingMap);

        mapGenerator.RevealFog(tilePosition, 2);
    }
}
