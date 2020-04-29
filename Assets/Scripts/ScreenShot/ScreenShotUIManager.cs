using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotUIManager : MonoBehaviour
{
    [SerializeField] GameObject ImageTemp;
    [SerializeField] Transform ImageParent;

    List<GameObject> ImageList = new List<GameObject>();
    private void Start()
    {
        DataManager.OnLoadImageCallback += OnLoadImage;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DataManager.Instance.OpenSaveDialog();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, 0.5f);
        }
    }
    public void OnLoadImage(Texture2D[] loadedTexture)
    {
        if(ImageList.Count > 0)
        {
            ClearImageList();
        }


        foreach(Texture2D texture in loadedTexture)
        {
            GameObject temp = Instantiate(ImageTemp, ImageParent);
            temp.GetComponent<Image>().sprite = Sprite.Create(texture,new Rect(0,0,500,500),Vector2.zero);
            temp.SetActive(true);
            ImageList.Add(temp);
        }
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 0.5f);
    }
    public void ClearImageList()
    {
       foreach(GameObject g in ImageList)
        {
            Destroy(g);
        }
        ImageList.Clear();
    }


}
