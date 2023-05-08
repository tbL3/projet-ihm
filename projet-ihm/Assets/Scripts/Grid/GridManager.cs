using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;

    [SerializeField] private List<Tile> tiles = new(2);

    [SerializeField] private Transform gameCamera;

    private Dictionary<Vector2, Tile> tilemap;

    public UnitScript selectedUnit;

    public GameObject stockUnits;

    public GameObject player;

    public Tilemap tileMapPaint;

    Node[,] graph;

    [SerializeField] private int[,] arrayGrid = new int[,] { { 1, 1, 1, 1, 1},
                                                             { 1, 1, 1, 1, 1},
                                                             { 1, 1, 1, 1, 1},
                                                             { 1, 1, 1, 1, 1},
                                                             { 1, 1, 1, 1, 1},
    { 1, 1, 1, 1, 1},
    { 1, 1, 1, 1, 1},};

    private void Start()
    {
        this.width = arrayGrid.GetLength(0); this.height = arrayGrid.GetLength(1);
        stockUnits.name = "UnitStock";
        GenerateGrid(arrayGrid);
        GeneratePathfindingGraph();
        
        
    }

    private void Update()
    {

    }


    public void GenerateGrid(int[,] arrayGrid)
    {
        tilemap = new Dictionary<Vector2, Tile>();
        
        for (int x = 0; x < width; x++)
        {
            print($"{tiles}");
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tiles[arrayGrid[x, y]],
                                              new Vector3(x*2, y*2),
                                              Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = ((x + y) % 2 == 1);

                spawnedTile.Init(isOffset);

                tilemap.Add(new Vector2(x, y), spawnedTile);
                spawnedTile.transform.localScale = new Vector3(2, 2, 0);
                spawnedTile.tileX = x;
                spawnedTile.tileY = y;
                spawnedTile.map = this;
                //tileMapPaint.SetTile(new Vector3Int(x, y), spawnedTile.tileBase);
            }
        }
        gameCamera.transform.position = new Vector3((float)width - 0.5f, (float)height - 0.5f, -10);
    }

    public void showUnitRange()
    {
        
    }

    public Tile GetTileAt(int x, int y)
    {
        return tiles[arrayGrid[x, y]];
    }
    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {

        Tile tt = tiles[arrayGrid[targetX/2, targetY/2]];

        if (UnitCanEnterTile(targetX / 2, targetY / 2) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        if (sourceX != targetX && sourceY != targetY)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost += 0.001f;
        }

        return cost;

    }

    public Vector2 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector2(x*2, y*2);
    }

    public bool UnitCanEnterTile(int x, int y)
    {

        // We could test the unit's walk/hover/fly type against various
        // terrain flags here to see if they are allowed to enter the tile.

        return tiles[arrayGrid[x, y]].isWalkable;
    }

    
    void GeneratePathfindingGraph()
    {
        // Initialize the array
        graph = new Node[width, height];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
                tiles[arrayGrid[x, y]].node = graph[x, y];
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                // This is the 4-way connection version:
                if(x > 0)
                    graph[x,y].neighbours.Add( graph[x-1, y] );
                if(x < width-1)
                    graph[x,y].neighbours.Add( graph[x+1, y] );
                if(y > 0)
                    graph[x,y].neighbours.Add( graph[x, y-1] );
                if(y < height-1)
                    graph[x,y].neighbours.Add( graph[x, y+1] );
                

                /*
                // This is the 8-way connection version (allows diagonal movement)
                // Try left
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < height - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                // Try Right
                if (x < width - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < height - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }

                // Try straight up and down
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < height - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);

                // This also works with 6-way hexes and n-way variable areas (like EU4)
                */
            }
        }
    }
    public void GeneratePathTo(int x, int y)
    {
        // Clear out our unit's old path.
        selectedUnit.currentPath = null;

        if (UnitCanEnterTile(x, y) == false)
        {
            // We probably clicked on a mountain or something, so just quit out.
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // Setup the "Q" -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.x,
                            selectedUnit.y
                            ];

        Node target = graph[
                            x,
                            y
                            ];

        dist[source] = 0;
        prev[source] = null;

        // Initialize everything to have INFINITY distance, since
        // we don't know any better right now. Also, it's possible
        // that some nodes CAN'T be reached from the source,
        // which would make INFINITY a reasonable value
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            // "u" is going to be the unvisited node with the smallest distance.
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;  // Exit the while loop!
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // If we get there, the either we found the shortest route
        // to our target, or there is no route at ALL to our target.

        if (prev[target] == null)
        {
            // No route between our target and the source
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        // Step through the "prev" chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
            
        }

        // Right now, currentPath describes a route from out target to our source
        // So we need to invert it!

        currentPath.Reverse();
        
        selectedUnit.currentPath = currentPath;
        selectedUnit.completedMovement = false;
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
