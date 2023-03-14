using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private List<Tile> tiles = new(2);

    [SerializeField] private Transform gameCamera;

    private Dictionary<Vector2, Tile> tilemap;

    [SerializeField] private int[,] arrayGrid = new int[,] { { 0, 0, 0, 0, 0},
                                                             { 0, 0, 0, 0, 0},
                                                             { 0, 0, 0, 0, 0},
                                                             { 0, 0, 0, 0, 0},
                                                             { 0, 0, 0, 0, 0},
    { 0, 0, 0, 0, 0},
    { 0, 0, 0, 0, 0},};

    private void Start()
    {
        this.width = arrayGrid.GetLength(0); this.height = arrayGrid.GetLength(1);
        generateGrid(arrayGrid);
    }

    public void generateGrid(int[,] arrayGrid)
    {
        tilemap = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tiles[arrayGrid[x, y]], new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = ((x + y) % 2 == 1);

                spawnedTile.Init(isOffset);

                tilemap.Add(new Vector2(x, y), spawnedTile);

            }
        }

        gameCamera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    /**public Tile getTileAtPos(Vector3 pos)
    {
        if(tiles.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }

        return null;
    }**/
}
