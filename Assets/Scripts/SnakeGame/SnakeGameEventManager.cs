using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeGameEventManager : MonoBehaviour
{

    public static SnakeGameEventManager Instance => instance;
    private static SnakeGameEventManager instance;

    public Vector2 MinSpawnPos,MaxSpawnPos;
    public GameObject FoodTemp;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SnakeGameUIController.Instance.OnHitPlayBut += StartGame;
    }

    public void StartGame()
    {

        SpawnFood();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SpawnFood();
        }
      
    }

    public void SpawnFood()
    {
        float rndX = Random.Range(MinSpawnPos.x, MaxSpawnPos.x);
        float rndY = Random.Range(MinSpawnPos.y, MaxSpawnPos.y);

        GameObject temp = Instantiate(FoodTemp);
        temp.transform.position = new Vector2(rndX, rndY);


    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        print("RestartGame");
    }
}
