using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private Vector2Int foodPosition;
    private GameObject growFoodToSpawn;

    private int width;
    private int height;
    private Snake snake;

    public void Setup(Snake snake)
    {
        this.snake = snake;
        SpawnFood();
    }

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

    }

    private void SpawnFood()
    {
        do
        {
            foodPosition = new Vector2Int(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height) / 2);
        } while (snake.GetSnakePosition().Contains(foodPosition));

        growFoodToSpawn = Object.Instantiate(GameAssets.instance.foodToGrow);
        growFoodToSpawn.tag = "GrowFood";
        growFoodToSpawn.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;
        growFoodToSpawn.transform.position = new Vector3(foodPosition.x, foodPosition.y);
    }

    public void RemoveAndRespawnFood()
    {
        Object.Destroy(growFoodToSpawn);
        SpawnFood();
        Debug.Log("Food eaten!");
    }
}
