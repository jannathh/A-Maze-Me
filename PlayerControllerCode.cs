using UnityEngine;
using UnityEngine.Tilemaps;

public class MazePlayerMovement : MonoBehaviour
{
    public float baseSpeed = 1f;
    public float maxSpeed = 6f;
    public float acceleration = 0.5f;
    public float deceleration = 0.2f;
    private Vector3 targetPosition;
    private Vector3Int currentCell;
    private Tilemap tilemap;
    public TileBase floorTile;

    private Vector2Int movementDirection = Vector2Int.zero;
    private float currentSpeed;
    private bool isMoving = false;
    private SpriteRenderer spriteRenderer;
    private bool isColliding = false;
    private float collisionCooldown = 0.5f;
    
    public void ResetSpeed() { 
        currentSpeed = baseSpeed; 
    }
    void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        currentCell = tilemap.WorldToCell(transform.position);
        targetPosition = transform.position;
        currentSpeed = baseSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    void Update()
    {
        if (isColliding)
        {
            collisionCooldown -= Time.deltaTime;
            if (collisionCooldown <= 0f) isColliding = false;
            return; 
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            HandleInput();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (!isMoving && currentSpeed > baseSpeed)
        {
            currentSpeed = Mathf.Max(baseSpeed, currentSpeed - deceleration * Time.deltaTime);
        }
    }

    void HandleInput()
    {
        string joyStickInput = JoystickManager.ReadJoystick();
        Vector2Int newDirection = Vector2Int.zero;
        if (joyStickInput == "UP")
        {
            newDirection = Vector2Int.up;
        }
        else if (joyStickInput == "DOWN")
        {
            newDirection = Vector2Int.down;
        }
        else if (joyStickInput == "LEFT")
        {
            newDirection = Vector2Int.left;
        }

        else if (joyStickInput == "RIGHT")
        {
            newDirection = Vector2Int.right;

        }

        if (newDirection != Vector2Int.zero)
        {
            if (newDirection == movementDirection)
            {
                currentSpeed = Mathf.Min(currentSpeed + acceleration, maxSpeed);
            }
            else
            {
                currentSpeed = baseSpeed;
            }

            TryMove(newDirection);
            RotateSprite(newDirection);
            movementDirection = newDirection;
            isMoving = true;
        }
        else
        {
            isMoving = false;
            ResetSpeed();
        }
    }

    void TryMove(Vector2Int direction)
    {
        Vector3Int targetCell = currentCell + new Vector3Int(direction.x, direction.y, 0);

        if (IsFloorTile(targetCell))
        {
            targetPosition = tilemap.CellToWorld(targetCell) + new Vector3(0.5f, 0.5f, 0);
            currentCell = targetCell;
        }
        else
        {
            isColliding = true;

            currentSpeed = baseSpeed; 

        }
    }

    void RotateSprite(Vector2Int direction)
    {
        float angle = 0f;

        if (direction == Vector2Int.up) angle = 0f;
        else if (direction == Vector2Int.down) angle = 180f;
        else if (direction == Vector2Int.left) angle = 90f;
        else if (direction == Vector2Int.right) angle = -90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    bool IsFloorTile(Vector3Int cell)
    {
        TileBase tile = tilemap.GetTile(cell);
        return tile == null || tile == floorTile;
    }
}
