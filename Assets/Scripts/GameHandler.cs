using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    private LevelGrid levelGrid;
    [SerializeField] private Snake snake;
    public TMP_Text text;
    private int score = 0;
    // Start is called before the first frame update
    
    
    void Start()
    {
        Debug.Log("Debug Start");
        levelGrid = new LevelGrid(25, 25);
        levelGrid.Setup(snake);
        snake.Setup(levelGrid, this);
        text.text = "Score " + score;

    }
    // Update is called once per frame
    void Update() 
    {
       //WIP trying to add a negative score food.
        /* if(score!=0 && score % 3 == 0 && !levelGrid.poisonUp())
        {
            levelGrid.SpawnFood(true);
        }*/
    }

    public void addScore() {
        score++;
        text.text = "Score "+ score;
    }
    public void minusScore()
    {
        score--;
        text.text = "Score " + score;
    }

    public void RestartGame() {
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
