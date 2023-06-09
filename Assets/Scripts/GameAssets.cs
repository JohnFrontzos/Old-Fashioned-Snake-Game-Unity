using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;

    private void Awake() {
        instance = this;
    }

    public Sprite snakeHeadSprite;
    public Sprite foodSprite;
    public GameObject foodToGrow;
    public GameObject foodToPoison;
    public Sprite snakeBodySprite;
    public GameObject snakeBody;
}
