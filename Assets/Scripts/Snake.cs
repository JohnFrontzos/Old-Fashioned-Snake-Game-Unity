using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerStep;
    private Vector2Int lastMoveDirection;
    public int gridMovementStep = 1;
    private LevelGrid level;


    public void Setup(LevelGrid levelGrid) {
        this.level = levelGrid;
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerStep = 0.5f;
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
        if (collision.CompareTag("GrowFood")) {
            level.RemoveAndRespawnFood();
        }
    }

    private void HandleSnakeMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerStep)
        {
            gridPosition += lastMoveDirection;
            gridMoveTimer -= gridMoveTimerStep;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(lastMoveDirection) - 90);
        }
    }

    private void HandleUserInput() {
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

    private float GetAngleFromVector(Vector2Int dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }


}
