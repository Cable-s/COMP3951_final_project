using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] internal Tilemap tilemap;
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap buildingMap;
    [SerializeField] private Tilemap fogOfWarMap;

    [SerializeField] internal TileBase waterTile, grasslandTile, forestTile, mountainTile, hoverTile;
    [SerializeField] private TileBase fogTile;

    private Grid grid;

    public int width        = 40;     // Width of the map in tiles
    public int height       = 20;     // Height of the map in tiles
    public float noiseScale = 1.5f;   // Adjusts the zoom level of the noise

    // Random offsets to change the starting point of the Perlin noise,
    // ensuring that a different pattern is generated each time
    private float offsetX;
    private float offsetY;

    private Vector3Int previousMousePos   = new Vector3Int();
    private static BuyMenuManager buyMenu = null;

    void Start()
    {
        grid = GetComponent<Grid>();

        // Generate random offsets using a large range to ensure variation
        offsetX = Random.Range(0f, 10000f);
        offsetY = Random.Range(0f, 10000f);

        if (buyMenu == null) buyMenu = Transform.FindAnyObjectByType<BuyMenuManager>();

        // Call GenerateMap to create the map on start
        GenerateMap();
    }
    void Update()
    {
        Vector3Int currentMousePosition = GetMousePosition();

        if (!currentMousePosition.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null);
            interactiveMap.SetTile(currentMousePosition, hoverTile);
            previousMousePos = currentMousePosition;
        }

        // Right mouse click -> remove building
        if (Input.GetMouseButton(1))
        {
            buildingMap.SetTile(currentMousePosition, null);
        }

    }

    void OnMouseDown()
    {
        Debug.Log("On Mouse Down -> buyMenu is called.");
        if (GetMousePosition().x >= Mathf.Floor(-1 * width / 2) && GetMousePosition().x <= Mathf.Floor(width / 2) && GetMousePosition().y >= Mathf.Floor(-1 * height / 2) && GetMousePosition().y <= Mathf.Floor(height / 2))
        {
            buyMenu.toggle();
        }
    }
    void GenerateMap()
    {
        // Loop through each tile position on the grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase selectedTile;

                // Calculate a noise sample for this tile position.
                // Adding offsetX and offsetY randomizes the noise pattern for each run.
                float sample = Mathf.PerlinNoise((x * noiseScale) + offsetX, (y * noiseScale) + offsetY);

                // Use thresholds to decide which tile to use based on the noise value
                if (sample < 0.25f)
                    selectedTile = waterTile;
                else if (sample < 0.5f)
                    selectedTile = grasslandTile;
                else if (sample < 0.75f)
                    selectedTile = forestTile;
                else
                    selectedTile = mountainTile;

                // Place the selected tile at the current position on the Tilemap
                Vector3Int tilePosition = new Vector3Int(x - width / 2, y - height / 2);

                tilemap.SetTile(tilePosition, selectedTile);

                fogOfWarMap.SetTile(tilePosition, fogTile);
            }
        }
    }

    private Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    public void RevealFog(Vector3Int centerCell, int vision)
    {
        for (int x = -vision; x <= vision; x++)
        {
            for (int y = -vision; y <= vision; y++)
            {
                Vector3Int cell = centerCell + new Vector3Int(x, y, 0);

                if (Vector3Int.Distance(centerCell, cell) <= vision)
                {
                    fogOfWarMap.SetTile(cell, null);
                }

            }
        }
    }
}



