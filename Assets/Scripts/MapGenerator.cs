using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

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

    [Header("Map Configuration")]
    public int width = 30;
    public int height = 30;
    public float noiseScale = 1.5f;

    private Grid grid;
    private float offsetX;
    private float offsetY;

    private Vector3Int previousMousePos = Vector3Int.zero;
    private static BuyMenuManager buyMenu = null;

    public Tilemap Tilemap => tilemap;
    public Tilemap FogOfWar => fogOfWarMap;

    private void Awake()
    {
        grid = GetComponent<Grid>();

        if (buyMenu == null)
            buyMenu = Transform.FindAnyObjectByType<BuyMenuManager>();

        // Add random offset to make noise-based terrain generation less uniform
        offsetX = Random.Range(0f, 10000f);
        offsetY = Random.Range(0f, 10000f);
    }

    private void Start()
    {
        GenerateMap();
        RevealFog(Vector3Int.zero, 4);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3Int currentMousePosition = GetMousePosition();

        // Update hover tile when mouse moves to a new cell
        if (!currentMousePosition.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null);
            interactiveMap.SetTile(currentMousePosition, hoverTile);
            previousMousePos = currentMousePosition;
        }

        // Right-click to remove buildings
        if (Input.GetMouseButton(1))
        {
            buildingMap.SetTile(currentMousePosition, null);
        }
    }

    private Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    private void OnMouseDown()
    {
        Vector3Int pos = GetMousePosition();

        // Ensure click is within bounds of the map
        if (pos.x >= Mathf.Floor(-width / 2f) && pos.x <= Mathf.Floor(width / 2f) &&
            pos.y >= Mathf.Floor(-height / 2f) && pos.y <= Mathf.Floor(height / 2f))
        {
            buyMenu.toggle();
        }
    }

    private void GenerateMap()
    {
        // Loop through all cells and assign tiles based on Perlin noise
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x - width / 2, y - height / 2, 0);

                // Add base tile
                baseMap.SetTile(tilePosition, baseTile);

                // Generate terrain using Perlin noise
                float sample = Mathf.PerlinNoise((x * noiseScale) + offsetX, (y * noiseScale) + offsetY);

                TileBase selectedTile = sample switch
                {
                    < 0.25f => waterTile,
                    < 0.5f => grasslandTile,
                    < 0.75f => forestTile,
                    _ => mountainTile
                };

                tilemap.SetTile(tilePosition, selectedTile);
                fogOfWarMap.SetTile(tilePosition, fogTile); // Start with fog covering everything
            }
        }
    }

    public void RevealFog(Vector3Int centerCell, int vision)
    {
        // Reveal fog in a circular area around the given center cell
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
