using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up,Down,Left,Right}

public class PlayerController : MonoBehaviour
{
   private static int _score;
    float speed = 2f;
    Direction playerDirection = Direction.Right;

    [SerializeField] Rigidbody2D rigid;
    [SerializeField] OnScreenController controller;
    // Start is called before the first frame update
    void Start()
    {
       SnakeGameUIController.Instance.OnHitPlayBut +=  Init;
    }

    public void Init()
    {
        OnSubscribe();
        SetVelocity(new Vector2(speed,0));
        controller.gameObject.transform.root.gameObject.SetActive(true);
    }
    public void OnDestroy()
    {
        OnUnsubscribe();
    }
    public void OnSubscribe()
    {
        PlayerInputHandler.OnDown += OnDown;
        PlayerInputHandler.OnUp += OnUp;
        PlayerInputHandler.OnRight += OnRight;
        PlayerInputHandler.OnLeft += OnLeft;

        controller.OnSwipRight += OnRight;
        controller.OnSwipLeft += OnLeft;
        controller.OnSwipDown += OnDown;
        controller.OnSwipUp += OnUp;

    }
    public void OnUnsubscribe()
    {
        PlayerInputHandler.OnDown -= OnDown;
        PlayerInputHandler.OnUp -= OnUp;
        PlayerInputHandler.OnRight -= OnRight;
        PlayerInputHandler.OnLeft -= OnLeft;
        controller.OnSwipRight -= OnRight;
        controller.OnSwipLeft -= OnLeft;
        controller.OnSwipDown -= OnDown;
        controller.OnSwipUp -= OnUp;

    }
    public void SetPlayerSpeed(float spd)
    {
        speed = spd;
    }
    public void SetVelocity(Vector2 speed)
    {
        rigid.velocity = speed;
    }
    public static void AddPlayerScore(int score)
    {
      
      _score += score;
        SnakeGameUIController.Instance.UpdateScore(_score);
    }

    void OnLeft()
    {
        if (playerDirection != Direction.Left && playerDirection != Direction.Right)
        {
            transform.eulerAngles = new Vector3(0,0,180);
            playerDirection = Direction.Left;
            SetVelocity(new Vector2(-speed, 0));
            print("Left");
        }
    }
    void OnRight()
    {
        if (playerDirection != Direction.Left && playerDirection != Direction.Right)
        {
            transform.eulerAngles = Vector2.zero;
            playerDirection = Direction.Right;
            SetVelocity(new Vector2(speed, 0));
            print("Right");
        }
    }
    void OnUp()
    {
        if (playerDirection != Direction.Up && playerDirection != Direction.Down)
        {
            transform.eulerAngles = new Vector3(0,0,90);
            playerDirection = Direction.Up;
            SetVelocity(new Vector2(0,speed));
            print("Up");
        }
    }
    void OnDown()
    {
        if (playerDirection != Direction.Up && playerDirection != Direction.Down)
        {
            transform.eulerAngles = new Vector3(0,0,270);
            playerDirection = Direction.Down;
            SetVelocity(new Vector2(0, -speed));
            print("Down");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Wall"))
        {
            SnakeGameEventManager.Instance.RestartGame();
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Food"))
        {
            collision.gameObject.SendMessage("PlusScore");
        }
    }

}
