using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private Tile tile;

    [SerializeField] private Transform gameCamera;

    private Dictionary<Vector2, Tile> tiles;

    private void Start()
    {
        generateGrid();
    }

    public void generateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < width; x++) 
        {

            for(int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = ((x + y) % 2 == 1);

                spawnedTile.Init(isOffset);

                tiles.Add(new Vector2(x, y), spawnedTile);

            }
        }

        gameCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    public Tile getTileAtPos(Vector3 pos)
    {
        if(tiles.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }

        return null;
    }
}
