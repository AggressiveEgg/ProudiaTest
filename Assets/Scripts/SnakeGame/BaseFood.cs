using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFood : MonoBehaviour
{
    [SerializeField]
    private int score;

    public int GetScore()
    {
        return Mathf.RoundToInt( score * GameController._ScoreMultiply);
    }
    public void PlusScore()
    {
        PlayerController.AddPlayerScore(GetScore());
        SnakeGameEventManager.Instance.SpawnFood();
        Destroy(gameObject);
    }

    
}
