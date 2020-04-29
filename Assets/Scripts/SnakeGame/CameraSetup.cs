using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
   [SerializeField] float WORLD_WIDTH = 12f;
    void Start()
    {
         //***Change this value based on how wide your world is***
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float cameraHeight = WORLD_WIDTH / aspectRatio;
        GetComponent<Camera>().orthographicSize = cameraHeight / 2f;
    }
}
