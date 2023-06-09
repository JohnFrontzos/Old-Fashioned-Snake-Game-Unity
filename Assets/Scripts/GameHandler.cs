using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    private LevelGrid levelGrid;
    [SerializeField] private Snake snake;
    // Start is called before the first frame update
    
    
    void Start()
    {
        Debug.Log("Debug Start");
        levelGrid = new LevelGrid(25, 25);
        levelGrid.Setup(snake);
        snake.Setup(levelGrid, this);

    }
    // Update is called once per frame
    void Update() 
    {
        
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
