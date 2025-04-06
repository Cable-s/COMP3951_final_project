using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using System.Net.NetworkInformation;
using System.Resources;

public class BuyMenuManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private Tilemap buildingMap;
    [SerializeField] private GameObject grassCard, mountainCard, forestCard, waterCard;
    //[SerializeField] private GameObject card;
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
        if (mapGenerator.FogOfWar.GetTile(tilePosition) != null || mapGenerator.BuildingMap.GetTile(tilePosition) != null)
        {
            this.CloseBuyMenu();
            return;
        }

        // Set the card content based on the tile's name.
        switch (tile.name) {
            case "grass_0":
                grassCard.SetActive(true);
                mountainCard.SetActive(false);
                forestCard.SetActive(false);
                waterCard.SetActive(false);
                break;
            case "water_0":
                grassCard.SetActive(false);
                mountainCard.SetActive(false);
                forestCard.SetActive(false);
                waterCard.SetActive(true);
                break;
            case "rock_0":
                grassCard.SetActive(false);
                mountainCard.SetActive(true);
                forestCard.SetActive(false);
                waterCard.SetActive(false);
                break;
            case "tree_0":
                grassCard.SetActive(false);
                mountainCard.SetActive(false);
                forestCard.SetActive(true);
                waterCard.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Handles the purchase event for a building.
    /// Instantiates a building based on the tile type, updates the map, and reveals additional fog.
    /// </summary>
    public void BuyEvent(string buildingName)
    {
        //Call building manager add building
        buildingManager.AddBuilding(buildingName, tilePosition);
        this.CloseBuyMenu();
    }

    public void CloseBuyMenu()
    {
        this.GetComponent<RectTransform>().transform.localScale = new Vector3(0, 0, 0);
        BuyMenuOpen = true;
    }
}
