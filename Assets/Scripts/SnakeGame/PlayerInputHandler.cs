using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInputHandler : MonoBehaviour
{

    public static Action OnLeft;
    public static Action OnRight;
    public static Action OnUp;
    public static Action OnDown;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            OnUp.SafeAction();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OnLeft.SafeAction();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnDown.SafeAction();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnRight.SafeAction();
        }
    }
}
