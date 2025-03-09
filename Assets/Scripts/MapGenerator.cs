using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap buildingMap;
    [SerializeField] private TileBase waterTile;
    [SerializeField] private TileBase grasslandTile;
    [SerializeField] private TileBase forestTile;
    [SerializeField] private TileBase mountainTile;
    [SerializeField] private TileBase hoverTile;
    [SerializeField] private TileBase building;

    private Grid grid;

    public int width = 40;          // Width of the map in tiles
    public int height = 40;         // Height of the map in tiles

    public float noiseScale = 0.5f; // Adjusts the zoom level of the noise

    // Random offsets to change the starting point of the Perlin noise,
    // ensuring that a different pattern is generated each time
    private float offsetX;
    private float offsetY;



    void Start()
    {
        grid = GetComponent<Grid>();

        // Generate random offsets using a large range to ensure variation
        offsetX = Random.Range(0f, 10000f);
        offsetY = Random.Range(0f, 10000f);

        // Call GenerateMap to create the map on start
        GenerateMap();
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
                tilemap.SetTile(new Vector3Int(x, y, 0), selectedTile);
            }
        }
    }

    private Vector3Int previousMousePos = new Vector3Int();

    void Update()
    {
        Vector3Int currentMousePosition = GetMousePosition();

        if (!currentMousePosition.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null);
            interactiveMap.SetTile(currentMousePosition, hoverTile);
            previousMousePos = currentMousePosition;
        }

        // Left mouse click -> add building
        if (Input.GetMouseButton(0))
        {
            buildingMap.SetTile(currentMousePosition, building);
        }

        // Right mouse click -> remove building
        if (Input.GetMouseButton(1))
        {
            buildingMap.SetTile(currentMousePosition, null);
        }
    }

    Vector3Int GetMousePosition()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return grid.WorldToCell(mouseWorldPos);

    }
}



