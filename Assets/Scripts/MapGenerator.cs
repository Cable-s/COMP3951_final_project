using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tilemap buildingMap;
    [SerializeField] internal TileBase waterTile, grasslandTile, forestTile, mountainTile, hoverTile, building;

    private Grid grid;

    public int width        = 20;     // Width of the map in tiles
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

        // Left mouse click -> add building
        if (Input.GetMouseButton(0))
        {
            print(currentMousePosition);
            if (currentMousePosition.x >= Mathf.Floor(-1 * width / 2) && currentMousePosition.x <= Mathf.Floor(width / 2) && currentMousePosition.y >= Mathf.Floor(-1 * height / 2) && currentMousePosition.y <= Mathf.Floor(height / 2)) {
                buildingMap.SetTile(currentMousePosition, building);
            }
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

        buyMenu.toggle(this);
    }

    /// <summary>
    /// Generates a tile-based map by using Perlin noise to determine biome distribution.
    /// </summary>
    /// <remarks>
    /// The method creates a coarse noise map that represents large-scale biome patterns,
    /// applies a blur to smooth transitions between different biomes,
    /// and then assigns a specific tile type (water, grassland, forest, or mountain)
    /// to each grid cell based on the blurred noise values.
    /// </remarks>
    void GenerateMap()
    {
        // Step 1: Create a coarse noise map to define the large-scale biome patterns.
        // An 2D array to store noise values for each tile.
        float[,] largeScaleNoise = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Generate a noise value using Perlin noise.
                // Multiplying x and y by 0.05 "zooms out" the noise to create larger clusters.
                // Adding offsetX and offsetY (random values) ensures a different pattern every run.
                largeScaleNoise[x, y] = Mathf.PerlinNoise((x * 0.05f) + offsetX, (y * 0.05f) + offsetY);
            }
        }

        // Step 2: Smooth the noise map by applying a blur.
        // This makes transitions between biomes more gradual by averaging neighboring values.
        float[,] blurredNoise = ApplyBlur(largeScaleNoise, 2);

        // Step 3: Use the blurred noise to assign a tile type to each cell based on predefined thresholds.
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase selectedTile;

                // Get the smoothed noise value for this cell.
                float noiseValue = blurredNoise[x, y];

                // Choose a tile based on the noise value:
                // - If the value is below 0.25, assign water.
                // - If the value is between 0.25 and 0.5, assign grassland.
                // - If the value is between 0.5 and 0.75, assign forest.
                // - If the value is above 0.75, assign mountain.
                if (noiseValue < 0.20f)
                    selectedTile = waterTile;
                else if (noiseValue < 0.45f)
                    selectedTile = grasslandTile;
                else if (noiseValue < 0.70f)
                    selectedTile = forestTile;
                else
                    selectedTile = mountainTile;

                // Place the selected tile on the tilemap at position (x, y).
                tilemap.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), selectedTile);
            }
        }
    }

    /// <summary>
    /// This method takes a 2D array of noise values and "blurs" it by averaging
    /// each cell with its neighbours. The idea is to remove small, abrupt changes and create a smoother, 
    /// more gradual transition between values. 
    /// </summary>
    /// 
    /// <param name="noise">The original 2D array of noise values.</param>
    /// 
    /// <param name="radius">
    /// The radius defining the neighborhood for the blur. A radius of 2 includes the cell itself and 
    /// all cells up to 2 units away in each direction.
    /// </param>
    /// 
    /// <returns>
    /// A new 2D array of floats with the same dimensions as the input, containing the smoothed (blurred) 
    /// noise values.
    /// </returns>
    /// 
    /// <remarks>
    /// How it works:
    /// It loops through each cell for every position (x, y) in the noise array and initialized a sum and a count 
    /// of how many values it's going to average. 
    /// 
    /// It checks neighbours within radius, looking at every cell within a square that extends radius tiles in all direction, 
    /// making sure not to go outside of the array's boundaries, and adding up all the noise values from the neighbouring cells 
    /// and keeps track of how many values are added. 
    /// 
    /// It then calculates the average by dividing the sum with count of values. This average value replaces the original value 
    /// at (x, y) in the resulting array.
    /// </remarks>
    private float[,] ApplyBlur(float[,] noise, int radius)
    {
        // An 2D array to store the blurred noise values.
        float[,] result = new float[width, height];

        // Iterate over each cell in the noise map.
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sum = 0;
                int count = 0;

                // Loop through the neighboring cells within the specified radius.
                for (int nx = -radius; nx <= radius; nx++)
                {
                    for (int ny = -radius; ny <= radius; ny++)
                    {
                        int sampleX = x + nx;
                        int sampleY = y + ny;

                        // Only include cells that are within the bounds of the noise array.
                        if (sampleX >= 0 && sampleX < width && sampleY >= 0 && sampleY < height)
                        {
                            sum += noise[sampleX, sampleY]; // Add the neighbor's noise value.
                            count++; // Count the number of cells included in the average.
                        }
                    }
                }

                // Calculate the average noise value for the cell and assign it to the result.
                result[x, y] = sum / count;
            }
        }

        // Return the blurred noise map.
        return result;
    }

    private Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    //void GenerateMap()
    //{
    //    // Loop through each tile position on the grid
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            TileBase selectedTile;

    //            // Calculate a noise sample for this tile position.
    //            // Adding offsetX and offsetY randomizes the noise pattern for each run.
    //            float sample = Mathf.PerlinNoise((x * noiseScale) + offsetX, (y * noiseScale) + offsetY);

    //            // Use thresholds to decide which tile to use based on the noise value
    //            if (sample < 0.25f)
    //                selectedTile = waterTile;
    //            else if (sample < 0.5f)
    //                selectedTile = grasslandTile;
    //            else if (sample < 0.75f)
    //                selectedTile = forestTile;
    //            else
    //                selectedTile = mountainTile;

    //            // Place the selected tile at the current position on the Tilemap
    //            tilemap.SetTile(new Vector3Int(x, y, 0), selectedTile);
    //        }
    //    }
    //}
}



