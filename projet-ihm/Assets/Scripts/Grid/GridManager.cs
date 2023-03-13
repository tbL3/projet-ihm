using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tile;

    [SerializeField] private Transform gameCamera;

    private void Start()
    {
        generateGrid();
    }

    public void generateGrid()
    {
        for(int x = 0; x < width; x++) 
        {

            for(int y =0; y < height; y++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = ((x + y) % 2 == 1);

                spawnedTile.Init(isOffset);

            }
        }

        gameCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }
}
