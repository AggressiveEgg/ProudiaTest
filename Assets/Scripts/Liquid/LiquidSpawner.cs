using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour
{
    [SerializeField] GameObject liquidTemp;
    [SerializeField] Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            LiquidSpawning();
        }
    }
    public void LiquidSpawning()
    {
        GameObject temp = Instantiate(liquidTemp,parent);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp.transform.position = pos;
    }
}
