using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

public class DataManager :MonoBehaviour
{
    public static DataManager Instance { get {

            return instance;
        } }
    private static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    private static string Path = "/Resources/GameData/";
    private static string PathPrefix = @"file://";
    public static System.Action<Texture2D[]> OnLoadImageCallback;
    

    public static byte[] Load(string fileName,string extraPath = "")
    {
        return File.ReadAllBytes(Application.dataPath + Path + extraPath + fileName);
    }

    public static bool CheckFileExit(string fileName, string extraPath = "")
    {
        try
        {
            byte[] b = Load(fileName);
          

        }
        catch
        {
          
        }
        return true;
    }

    public static Texture2D TakeScreenShot()
    {
        

        Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture();
        Texture2D texture = new Texture2D(2048,2048, TextureFormat.RGB24, true);
        texture.ReadPixels(Screen.safeArea, 0, 0);
        texture.Apply();
        UnityEngine.Debug.Log("Cam Rect " + Screen.safeArea);

        // Read screen contents into the texture
        return texture;
    }
    public void OpenSaveDialog()
    {
        Texture2D texture = TakeScreenShot();

        string fileName = "ScreenShot";

        System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();

        sfd.Title = "Save Screen Shot";
        sfd.Filter = "png files (*.png)|*.png|txt files (*.txt)|*.txt|All files (*.*)|*.*";
        sfd.FilterIndex = 1;
        sfd.RestoreDirectory = true;

        Stream myStream;
        var path = "";
        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            if ((myStream = sfd.OpenFile()) != null)
            {

                var file = texture.EncodeToPNG();
                myStream.Write(file, 0, file.Length);
                path = sfd.FileName;
                print(path);
                Process.Start(@path);
                if (path.Length > 0)
                {
                    path = StepBackward(path);

                    string[] files = GetAllFileInPath(path);

                    StartCoroutine(LoadImages(files, OnLoadImageCallback));
                }


                myStream.Close();
            }
        }
        /*
         path = EditorUtility.SaveFilePanel(
            "Save ScreenShot",
            "",
            fileName + ".png",
            "png");
        UnityEngine.Debug.Log(path);

    */


        /* if (path.Length != 0)
         {
             var pngData = texture.EncodeToPNG();
             if (pngData != null)
             {
                 File.WriteAllBytes(path, pngData);
                 Process.Start(@path);
                 UnityEngine.Debug.Log("Save Data : " + path);
             }
         }*/
        
    }
    private static string StepBackward(string path)
    {
        for(int i = path.Length-1;i >0;i--)
        {
            if (path[i] == '/' || path[i] == '\\')
            {
                return path;
            }
            else
            {
                path = path.Remove(i) ;
              
            }
        }

        return path;
    }
    public static string[] GetAllFileInPath(string path,string prefix = ".png")
    {
        string[] files;

        try
        {
            files = System.IO.Directory.GetFiles(path, "*" + prefix);
            foreach(string file in files)
            {
                UnityEngine.Debug.Log(file);
            }
            return files;
        }
        catch
        {
            return null;
        }
        
    }

   public  IEnumerator LoadImages(string[] filesPath,System.Action<Texture2D[]> result)
    {
        //load all images in default folder as textures and apply dynamically to plane game objects.
        //6 pictures per page
        Texture2D[] textList = new Texture2D[filesPath.Length];

        int index = 0;
        foreach (string tstring in filesPath)
        {

            string pathTemp = PathPrefix + tstring;
            WWW www = new WWW(pathTemp); 
            yield return www;
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
            www.LoadImageIntoTexture(texTmp);

            textList[index] = texTmp;

            

            index++;

            if(index == filesPath.Length && result != null)
            {
                UnityEngine.Debug.Log("Load Data Done");
                result(textList);
            }
        }

    }

 

}
public static class DataManagerExtendsion
{
    private static byte[] EncodeString(this string data)
    {
        UnityEngine.Debug.Log(data);
        byte[] b = Encoding.ASCII.GetBytes(data);

        return b;
    }
    private static string DecodeString(this byte[] data)
    {

        string b = Encoding.ASCII.GetString(data);

        return b;
    }
}
