using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerStep;
    private Vector2Int lastMoveDirection;

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerStep = 1f;
        gridMoveTimer = gridMoveTimerStep;
        lastMoveDirection = new Vector2Int(0, 1);
    }

    private void Update()
    {
        HandleUserInput();
        HandleSnakeMovement();
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
            if (lastMoveDirection.y != -1)
            {
                lastMoveDirection.x = 0;
                lastMoveDirection.y = +1;

            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (lastMoveDirection.y != +1)
            {
                lastMoveDirection.x = 0;
                lastMoveDirection.y = -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (lastMoveDirection.x != -1)
            {
                lastMoveDirection.x = +1;
                lastMoveDirection.y = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (lastMoveDirection.x != -1)
            {
                lastMoveDirection.x = -1;
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
