using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static float _ScoreMultiply = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public static class ExtendsionClass
{
    public static void SafeAction(this Action action)
    {
        if(action != null)
        {
            action();
        }
    }
}