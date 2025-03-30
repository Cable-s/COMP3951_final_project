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

    // Signleton instace for global access
    public static BuyMenuManager Instance { get; private set; }

    /// <summary>
    /// Ensures a single instance of BuyMenuManger exits.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Toggles the buy menu on or off.
    /// Caches the mouse position conversion to avoid duplicate calls.
    /// </summary>
    public void toggle()
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
            this.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            BuyMenuOpen = true;
            UpdateCard();
    }

    /// <summary>
    /// Updates the UI card information based on the currently selected tile.
    /// </summary>
    private void UpdateCard()
    {
        // This if statement prevents player from placing building when there is a fog
        if (mapGenerator.FogOfWar.GetTile(tilePosition) != null)
        {
            return;
        }

        TextMeshProUGUI[] ts = this.transform.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI t in ts)
        {
            if (t.name == "CardName") cardName = t;
            if (t.name == "Price") price = t;
            if (t.name == "PriceAmount") priceAmount = t;
        }

        // Set the card content based on the tile's name.
        switch (tile.name) {
            case "grass_0":
                cardName.text = "grassland Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
            case "water_0":
                cardName.text = "water Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
            case "rock_0":
                cardName.text = "mountain Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
            case "forest_0":
                cardName.text = "forest Building";
                price.text = "People";
                priceAmount.text = "5";
                break;
        }
    }

    /// <summary>
    /// Handles the purchase event for a building.
    /// Instantiates a building based on the tile type, updates the map, and reveals additional fog.
    /// </summary>
    public void BuyEvent()
    {
        if (tile.name == "grass_0")
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
        else if (tile.name == "tree_0")
        {
            IBuilding building = new ForestBuilding();
            buildingContext.setBuilding(building);
        }

        buildingContext.addBuildingToTile(tilePosition, buildingMap);

        // Do not remove water tile when placing building on top
        if (tile.name != "water_0")
        {
            mapGenerator.Tilemap.SetTile(tilePosition, null);
        }

        mapGenerator.RevealFog(tilePosition, 2); // reveal a small area of fog after a building is bought
    }

    public void CloseBuyMenu()
    {
        this.GetComponent<RectTransform>().transform.localScale = new Vector3(0, 0, 0);
        BuyMenuOpen = true;
    }
}
