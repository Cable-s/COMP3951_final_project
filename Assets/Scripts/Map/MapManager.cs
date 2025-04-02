using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

/// <summary>
/// Handles map generation, tile placement, and user interactions (such as hovering and clicking) on the map.
/// Also manages the fog of war reveal functionality.
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [Header("Tilemap References")]
    [SerializeField] private Tilemap tilemap;         
    [SerializeField] private Tilemap interactiveMap;  
    [SerializeField] private Tilemap buildingMap;     
    [SerializeField] private Tilemap fogOfWarMap;     
    [SerializeField] private Tilemap baseMap;

    [Header("Tile Assets")]
    [SerializeField] private TileBase baseTile;
    [SerializeField] internal TileBase waterTile, grasslandTile, forestTile, mountainTile, hoverTile;
    [SerializeField] private TileBase fogTile;

    [Header("Managers")]
    [SerializeField] private BuildingManager buildingManager;

    [Header("Map Configuration")]
    public int width = 30;          // Width of the map in cells.
    public int height = 30;         // Height of the map in cells.
    public float noiseScale = 1.5f; // Scale factor for Perlin noise used in terrain generation.

    private Grid grid;
    private float offsetX;
    private float offsetY;

    private Vector3Int previousMousePos = Vector3Int.zero;
    private static BuyMenuManager buyMenu = null;

    private Camera mainCamera;

    public Tilemap Tilemap => tilemap;
    public Tilemap FogOfWar => fogOfWarMap;
    public Tilemap BuildingMap => buildingMap;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Caches the Grid component and locates the BuyMenuManager if not already set.
    /// Also initializes random offsets for noise-based terrain generation.
    /// </summary>
    private void Awake()
    {
        grid = GetComponent<Grid>();
        mainCamera = Camera.main;

        if (buyMenu == null)
            buyMenu = BuyMenuManager.Instance;

        offsetX = Random.Range(0f, 10000f);
        offsetY = Random.Range(0f, 10000f);
    }

    /// <summary>
    /// Called on the frame when a script is enabled just before any of the Update methods are called.
    /// Generates the map and reveals an initial fog area.
    /// </summary>
    private void Start()
    {
        GenerateMap();
        RevealFog(Vector3Int.zero, 4); // Reveals fog around the origin cell with a vision radius of 4.
    }

    /// <summary>
    /// Called every frame.
    /// Handles user input for hovering over tiles and right-click removal of buildings.
    /// </summary>
    private void Update()
    {
        Vector3Int currentMousePosition = GetMousePosition();

        // Update hover tile if the mouse has moved to a new cell.
        if (!currentMousePosition.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null); // remove hover effect from the previous cell.
            interactiveMap.SetTile(currentMousePosition, hoverTile);  // st hover effect on the current cell.
            previousMousePos = currentMousePosition;
        }

        // Right-click (mouse button 1) removes building tiles at the current mouse cell.
        if (Input.GetMouseButton(1))
        {
            buildingManager.RemoveBuiding(currentMousePosition);
        }
    }

    /// <summary>
    /// Converts the current mouse screen position into cell coordinates on the grid.
    /// </summary>
    /// <returns>Grid cell coordinates corresponding to the mouse position.</returns>
    private Vector3Int GetMousePosition()
    {
        // Convert the mouse position from screen space to world space.
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Convert the world position to grid cell coordinates.
        return grid.WorldToCell(mouseWorldPos);
    }

    /// <summary>
    /// Called when the mouse is clicked over the GameObject.
    /// Toggles the buy menu if the click is within the map bounds.
    /// </summary>
    private void OnMouseDown()
    {
        Vector3Int pos = GetMousePosition();

        // Check if the click is within the defined bounds of the map.
        if (pos.x >= Mathf.Floor(-width / 2f) && pos.x <= Mathf.Floor(width / 2f) &&
            pos.y >= Mathf.Floor(-height / 2f) && pos.y <= Mathf.Floor(height / 2f))
        {
            // Toggle the buy menu. This is a direct call and assumes buyMenu is valid.
            buyMenu.toggle();
        }
    }

    /// <summary>
    /// Generates the map by looping through all cells, applying a base tile, and
    /// selecting terrain tiles based on Perlin noise.
    /// </summary>
    private void GenerateMap()
    {
        // Loop through each cell based on the specified width and height.
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Calculate the cell position so that the map is centered.
                Vector3Int tilePosition = new Vector3Int(x - width / 2, y - height / 2, 0);

                // Place the base tile on the base map.
                baseMap.SetTile(tilePosition, baseTile);

                // Generate a Perlin noise sample for the current cell.
                float sample = Mathf.PerlinNoise((x * noiseScale) + offsetX, (y * noiseScale) + offsetY);

                // Select a tile based on the noise value.
                TileBase selectedTile = sample switch
                {
                    < 0.25f => waterTile,
                    < 0.5f => grasslandTile,
                    < 0.75f => forestTile,
                    _ => mountainTile
                };

                // Set the selected terrain tile on the main tilemap.
                tilemap.SetTile(tilePosition, selectedTile);
                // Cover the entire map with fog initially.
                fogOfWarMap.SetTile(tilePosition, fogTile);
            }
        }
    }

    /// <summary>
    /// Reveals the fog of war in a circular area around the specified center cell.
    /// </summary>
    /// <param name="centerCell">The center cell from which to reveal fog.</param>
    /// <param name="vision">The radius (in cells) within which to reveal fog.</param>
    public void RevealFog(Vector3Int centerCell, int vision)
    {
        // Loop over a square region around the center cell.
        for (int x = -vision; x <= vision; x++)
        {
            for (int y = -vision; y <= vision; y++)
            {
                Vector3Int cell = centerCell + new Vector3Int(x, y, 0);

                // Only reveal cells within the circular area defined by the vision radius.
                if (Vector3Int.Distance(centerCell, cell) <= vision)
                {
                    fogOfWarMap.SetTile(cell, null);
                }
            }
        }
    }
}
