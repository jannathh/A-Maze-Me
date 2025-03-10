using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    public int width = 20; 
    public int height = 20; 
    public Tilemap tilemap;
    public AnimatedTile wallTile;
    public AnimatedTile floorTile;

    public GameObject playerPrefab; 
    public GameObject goalPrefab;   

    private GameObject currentPlayer;
    private GameObject currentGoal;

    private int[,] maze;
    private List<Vector3Int> floorPositions = new List<Vector3Int>();

    public static MazeGenerator Instance { get; private set; }

    void Start()
    {
        Initialize();
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        GenerateMaze();
        DrawMaze();
        Destroy(currentGoal);
        Destroy(currentPlayer);
        SpawnPlayerAndGoal();
    }

    public void DestroyGoalAndPlayer()
    {
        Destroy(currentGoal);
        Destroy(currentPlayer);
    }

    void GenerateMaze()
    {
        maze = new int[width, height];

        // Fill grid with walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; 
            }
        }

        CarvePath(1, 1);
    }

    void CarvePath(int x, int y)
    {
        maze[x, y] = 0; 

        foreach (Vector2Int dir in ShuffleDirections())
        {
            int nx = x + dir.x * 2;
            int ny = y + dir.y * 2;

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny] == 1)
            {
                maze[x + dir.x, y + dir.y] = 0; // Carve the wall
                CarvePath(nx, ny); // Recur
            }
        }
    }

    Vector2Int[] ShuffleDirections()
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        for (int i = 0; i < directions.Length; i++)
        {
            int rnd = Random.Range(0, directions.Length);
            Vector2Int temp = directions[rnd];
            directions[rnd] = directions[i];
            directions[i] = temp;
        }
        return directions;
    }

    void DrawMaze()
    {
        tilemap.ClearAllTiles();
        floorPositions.Clear(); 

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                if (maze[x, y] == 1)
                {
                    tilemap.SetTile(tilePos, wallTile);
                }
                else
                {
                    tilemap.SetTile(tilePos, floorTile);
                    floorPositions.Add(tilePos);
                }
            }
        }
    }

    void SpawnPlayerAndGoal()
    {
        // Randomly select two distinct floor positions
        int playerIndex = Random.Range(0, floorPositions.Count);
        int goalIndex;
        do
        {
            goalIndex = Random.Range(0, floorPositions.Count);
        } while (goalIndex == playerIndex);

        // Instantiate player and goal
        currentPlayer = Instantiate(playerPrefab, tilemap.CellToWorld(floorPositions[playerIndex]) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        currentGoal = Instantiate(goalPrefab, tilemap.CellToWorld(floorPositions[goalIndex]) + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.R))
        {
            Initialize();
        }
    }
}
