using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerStep = 0.25f;
    private Vector2Int lastMoveDirection;
    public int gridMovementStep = 1;
    private LevelGrid level;
    bool isOutOfCamera = false;
    private int snakeBodySize = 1;
    private List<Vector2Int> snakeMovePositionList = new();
    private GameHandler gameHandler;

    public void Setup(LevelGrid levelGrid, GameHandler gameHandler)
    {
        this.level = levelGrid;
        this.gameHandler = gameHandler;
    }

    public void OnBecameInvisible()
    {
        Debug.Log("Where are you going?");
        isOutOfCamera = true;
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimer = gridMoveTimerStep;
        lastMoveDirection = new Vector2Int(0, 1);
    }

    private void Update()
    {
        HandleUserInput();
        HandleSnakeMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Snake hit something");
        if (collision.CompareTag("GrowFood"))
        {
            Debug.Log("It's food!");
            snakeBodySize++;
            level.RemoveAndRespawnFood();
            gameHandler.addScore();
        }
        else if (collision.CompareTag("Poison"))
        {
            Debug.Log("It's poison!");
            level.RemoveAndRespawnFood(true);
            gameHandler.minusScore();
        }
        else if (collision.CompareTag("Snake"))
        {
            Debug.Log("Its self!");
            gameHandler.RestartGame();
        }
    }

    private void HandleSnakeMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerStep)
        {
            snakeMovePositionList.Insert(0, gridPosition);
            if (isOutOfCamera)
            {
                if (Mathf.Abs(gridPosition.x) > Mathf.Abs(gridPosition.y))
                {
                    gridPosition = new Vector2Int(gridPosition.x * -1, gridPosition.y);
                }
                else
                {
                    gridPosition = new Vector2Int(gridPosition.x, gridPosition.y * -1);
                }
                isOutOfCamera = false;
            }
            else
            {
                gridPosition += lastMoveDirection;
                gridMoveTimer -= gridMoveTimerStep;
            }
            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            DrawSnakeBody();
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(lastMoveDirection) - 90);
        }
    }

    private void DrawSnakeBody()
    {
        for (int i = 0; i < snakeMovePositionList.Count; i++)
        {
            GameObject snakeBody = Object.Instantiate(GameAssets.instance.snakeBody);
            snakeBody.transform.position = new Vector3(snakeMovePositionList[i].x, snakeMovePositionList[i].y);
            snakeBody.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(lastMoveDirection) - 90);
            Object.Destroy(snakeBody, gridMoveTimerStep);
        }
    }

    private void HandleUserInput()
    {

        // TODO maybe use the InputSystem
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (lastMoveDirection.y != -gridMovementStep)
            {
                lastMoveDirection.x = 0;
                lastMoveDirection.y = +gridMovementStep;

            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (lastMoveDirection.y != +gridMovementStep)
            {
                lastMoveDirection.x = 0;
                lastMoveDirection.y = -gridMovementStep;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (lastMoveDirection.x != -gridMovementStep)
            {
                lastMoveDirection.x = +gridMovementStep;
                lastMoveDirection.y = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (lastMoveDirection.x != -gridMovementStep)
            {
                lastMoveDirection.x = -gridMovementStep;
                lastMoveDirection.y = 0;
            }
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition() { return gridPosition; }

    public List<Vector2Int> GetSnakePosition()
    {
        List<Vector2Int> snakeTotalPosition = new();
        snakeTotalPosition.Add(gridPosition);
        return snakeMovePositionList;
    }
}
