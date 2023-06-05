using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    private LevelGrid levelGrid;
    [SerializeField] private Snake snake;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Debug Start");
        levelGrid = new LevelGrid(25, 25);
        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

    }
    // Update is called once per frame
    void Update() 
    {
        
    }
}
