using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    //serialize Field makes private variables visable to the unity editor without making them public.
    //these fields are initialized in the Unity editor
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Tile tile;
    [SerializeField] private Transform camera;
    //start is a default entry point for Unity
    private void Start()
    {
        generateGrid();
    }


    void generateGrid() {
        float scaleX = tile.transform.localScale.x;
        float scaleY = tile.transform.localScale.y;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                //instantiate a new tile at x, y. Quaternions are for rotations which are not needed
                var spawnedTile = Instantiate(tile, new Vector3(x * scaleX, y * scaleY), Quaternion.identity);

                //get tile scale x
                scaleX = tile.transform.localScale.x;
                scaleY = tile.transform.localScale.y;


                //give each tile a unique name in the editor
                spawnedTile.name = $"Tile {x} {y}";

                //create a checkboard pattern
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
        print("done");
        //center the camera around the game board
        camera.transform.position = new Vector3((float)width * scaleX / 2 - 0.5f, (float)height * scaleY / 2 -0.5f, -10);
    }
}

